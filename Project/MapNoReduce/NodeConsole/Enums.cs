using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PADIMapNoReduce
{
    enum ServerState
    {
        ALIVE, 
        FREEZE
    }

    enum ServerRole
    {
        WORKER, 
        JOB_TRACKER, 
        NONE
    }

    enum ExecutionState
    {
        WAITING,
        WORKING, 
        WAITING_FOR_SPLIT, 
        RETURNING_SPLIT
    }
    
}
