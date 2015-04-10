﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace PADIMapNoReduce
{
    class TreeViewManager
    {
        public static string TIMESTAMP_TAG = "TIME";


        public static string ACTIVE_WORKERS_TAG = "ACTIVE_WORKERS";
        private static Color ACTIVE_WORKERS_COLOR = Color.Green;
        
        public static string DOWN_WORKERS_TAG = "DOWN_WORKERS";
        private static Color DOWN_WORKERS_COLOR = Color.Red;
        
        
        public static string RING_NEXT_URL_TAG = "RING_N_URL";
        private static Color RING_NEXT_COLOR = Color.SteelBlue;
        
        public static string RING_NEXT_NEXT_URL_START1_TAG = "RING_NN_URL_1";
        private static Color RING_NEXT_NEXT_1_COLOR = Color.DodgerBlue;
        
        public static string RING_NEXT_NEXT_URL_START2_TAG = "RING_NN_URL_2";
        private static Color RING_NEXT_NEXT_2_COLOR = Color.SkyBlue;
        
        public static string CURRENT_JTS_TAG = "CURRENT_JTS";
        private static Color CURRENT_JTS_COLOR = Color.Gainsboro;

        private TreeView NetworkTreeView;

        private TreeNode mostRecentNode;
        public TreeViewManager(TreeView t)
        {
            this.NetworkTreeView = t;
        }

        private string GenerateTimeStamp()
        {
            return DateTime.Now.ToString("HH:mm:ss");
        }

        private TreeNode CreateNode(string id, string tag)
        {
            TreeNode rootNode = new TreeNode();
            rootNode.Text = id;
            rootNode.Name = id;
            rootNode.Tag = tag;

            return rootNode;
        }

        private void AddNodeRepresentation(string rootNodeKey, NodeRepresentation node)
        {
            TreeNode root = TreeViewUtil.FindNode(NetworkTreeView, rootNodeKey);
            
            if (root == null)
            {
                Logger.LogErr("Something bad happened generating tree: cant find rootNode " + rootNodeKey);
                return;
            }

            string id = "";
            node.info.TryGetValue(NodeRepresentation.ID, out id);

            TreeNode nodeTree = CreateNode(id, rootNodeKey + ":" + id);
            foreach (TreeNode t in NodeAtributesRepresentationToTree(node))
            {
                nodeTree.Nodes.Add(t);
            }

            root.Nodes.Add(nodeTree);
            root.Expand();
        }

        private List<TreeNode> NodeAtributesRepresentationToTree(NodeRepresentation node)
        {
            IDictionary<string, string> values = node.info;
            List<TreeNode> result = new List<TreeNode>();

            foreach (KeyValuePair<string, string> entry in values)
            {
                string fieldName = entry.Key;
                string value = entry.Value;

                TreeNode field = CreateNode(fieldName, fieldName);
                field.Text = fieldName + " = " + value;
                result.Add(field);
            }

            return result;
        }


        public void RefreshNetWorkConfiguration()
        {
            //collapse
            NetworkTreeView.CollapseAll();

            string timeStamp = TIMESTAMP_TAG + ":" + GenerateTimeStamp();
            TreeNode now = CreateNode(timeStamp, timeStamp);
            NetworkTreeView.Nodes.Add(now);

            
            IDictionary<string, NodeRepresentation> knownNodes = NetworkManager.GetKnownWorkers();
            List<string> downNodes = NetworkManager.GetDownWorkers();
            List<string> currentJTs = new List<string>();


            //check if active nodes
            if (knownNodes.Count != 0 && knownNodes.Count != downNodes.Count)
            {
                string activeTag = ACTIVE_WORKERS_TAG + ":" + timeStamp;
                TreeNode active = CreateNode(ACTIVE_WORKERS_TAG, activeTag);
                active.BackColor = ACTIVE_WORKERS_COLOR;
                now.Nodes.Add(active);

                //Generating Active workers
                foreach (KeyValuePair<string, NodeRepresentation> entry in knownNodes)
                {
                    //check if it not flagged as down
                    if (!downNodes.Contains(entry.Key))
                    {
                        AddNodeRepresentation(activeTag, entry.Value);

                        string jobTracker = "";
                        entry.Value.info.TryGetValue(NodeRepresentation.CURRENT_JT, out jobTracker);

                        if (jobTracker != null && !currentJTs.Contains(jobTracker))
                        {
                            currentJTs.Add(jobTracker);
                        }
                    }
                }
            }
            
            if (downNodes.Count != 0)
            {
                string downTag = DOWN_WORKERS_TAG + ":" + timeStamp;
                TreeNode down = CreateNode(DOWN_WORKERS_TAG, downTag);
                down.BackColor = DOWN_WORKERS_COLOR;
                now.Nodes.Add(down);
                
                foreach (string id in downNodes)
                {
                    AddNodeRepresentation(downTag, knownNodes[id]);
                }
            }

            if (currentJTs.Count != 0)
            {
                string jtTag = CURRENT_JTS_TAG + ":" + timeStamp;
                TreeNode jt = CreateNode(CURRENT_JTS_TAG, jtTag);
                jt.BackColor = CURRENT_JTS_COLOR;
                now.Nodes.Add(jt);

                foreach (string id in currentJTs)
                {
                    if (knownNodes.ContainsKey(id))
                    {
                        AddNodeRepresentation(jtTag, knownNodes[id]);
                    }
                    else
                    {
                        NodeRepresentation node = new NodeRepresentation();
                        node.info.Add("UNKNOWN JOBTRACKER WORKER ID", id);
                        AddNodeRepresentation(jtTag, node);
                    }
                }
            }

            now.Expand();
            mostRecentNode = now;
        }

        public string GenerateNextUrlGraph(IDictionary<string, NodeRepresentation> knownNodes, NodeRepresentation node)
        {
            int numberOfTimes = knownNodes.Count*2 + 1;
            
            string id;
            string nextUrl;
            string result = "";
            for (int i = 0; i < numberOfTimes; i++)
            {
                node.info.TryGetValue(NodeRepresentation.ID, out id);
                node.info.TryGetValue(NodeRepresentation.NEXT_URL, out nextUrl);

                result += " => " + id;

                if (nextUrl == null || (nextUrl != null && nextUrl == ""))
                {
                    Logger.LogErr("Node " + id + " as no nextUrl!");
                    break;
                }

                knownNodes.TryGetValue(nextUrl, out node);

                if (node == null)
                {
                    Logger.LogErr("No information on nextUrl...");
                    break;
                }
            }

            return result;
        }

        public string GenerateNextNextUrlGraph(IDictionary<string, NodeRepresentation> knownNodes, NodeRepresentation node /*, string fieldName, string separator */)
        {
            int numberOfTimes = knownNodes.Count*2 + 1;

            string id = "";
            string nextNextUrl = "";
            string result = "";
            for (int i = 0; i < numberOfTimes; i++)
            {
                node.info.TryGetValue(NodeRepresentation.ID, out id);
                node.info.TryGetValue(NodeRepresentation.NEXT_NEXT_URL, out nextNextUrl);

                result += " ==> " + id;

                if (nextNextUrl == null || (nextNextUrl != null && nextNextUrl == ""))
                {
                    Logger.LogErr("Node " + id + " has no nextNextUrl!");
                    break;
                }

                knownNodes.TryGetValue(nextNextUrl, out node);

                if (node == null)
                {
                    Logger.LogErr("No information on nextNextUrl...");
                    break;
                }
            }

            return result;
        }



        public void GenerateGraph()
        {
            //reconstruction of the ring again
            IDictionary<string, NodeRepresentation> knownNodes = NetworkManager.GetKnownUrlWorkers();

            if (knownNodes.Count == 0)
            {
                Logger.LogErr("There are no known nodes");
                return;
            }

            NodeRepresentation node = knownNodes[knownNodes.Keys.First(t => true)];


            //Generating nextUrlGraph
            string result = GenerateNextUrlGraph(knownNodes, node);
            TreeNode tagNode = CreateNode(RING_NEXT_URL_TAG + " : " + result, RING_NEXT_URL_TAG);
            tagNode.BackColor = RING_NEXT_COLOR;
            mostRecentNode.Nodes.Add(tagNode);

            //Generating nextNextUrlGraph 1
            string resultNextNextUrl1 = GenerateNextNextUrlGraph(knownNodes, node);
            TreeNode tagNode1 = CreateNode(RING_NEXT_NEXT_URL_START1_TAG + " : " + resultNextNextUrl1, RING_NEXT_NEXT_URL_START1_TAG);
            tagNode1.BackColor = RING_NEXT_NEXT_1_COLOR;
            mostRecentNode.Nodes.Add(tagNode1);

            //Generating nextNextUrlGraph 2
            string nextUrl;
            node.info.TryGetValue(NodeRepresentation.NEXT_URL, out nextUrl);
            NodeRepresentation node2;

            if (nextUrl == null) {
                Logger.LogErr("Not enough information to process nextNextGraph 2");
                return;
            }

            knownNodes.TryGetValue(nextUrl, out node2);
            string resultNextNextUrl2 = GenerateNextNextUrlGraph(knownNodes, node2);
            TreeNode tagNode2 = CreateNode(RING_NEXT_NEXT_URL_START2_TAG + " : " + resultNextNextUrl2, RING_NEXT_NEXT_URL_START2_TAG);
            tagNode2.BackColor = RING_NEXT_NEXT_2_COLOR;
            mostRecentNode.Nodes.Add(tagNode2);
        }

        public void Clear()
        {
            NetworkTreeView.Nodes.Clear();
        }
    }
}
