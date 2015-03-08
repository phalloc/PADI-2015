using System;
using System.Threading;
using System.Collections.Generic;


/*
 * A thread pool ThrPool é inicializada com um conjunto de N threads.
A aplicação submete um delegate do tipo ThrWork para execução assíncrona invocando o método AssyncInvoke.
Os pedidos de invocação são colocados num buffer gerido de forma circular.
Threads que estejam livres podem consumir pedidos, caso contrário ficam bloqueadas.
Quando uma thread consome um pedido, retira-o do buffer e executa o pedidoe volta a verificar se existem pedidos pendentes.
 
 */

public delegate void ThrWork();

class ThrPool{

    private List<ThrWork> queue;
    private List<Thread> threads;
   
    public ThrPool(int thrNum, int bufSize){

        queue = new List<ThrWork>();
        threads = new List<Thread>();

        // initialize
        for (int i = 0; i < thrNum; i++) {
            Thread t = new Thread(threadLoop);
            threads.Add(t);
        }
    }

    private ThrWork getWorkFromQueue()
    {
        lock (queue) {  
            if (queue.Count == 0){
                return null;
            }else{
                ThrWork work = queue[0];
                queue.RemoveAt(0);

                return work;
            }
        }
         

    }

    public void AssyncInvoke(ThrWork action){
        queue.Add(action);
    }

    private void threadLoop(){
        
        while (true) {
            ThrWork work = getWorkFromQueue();
            
            //my work here is done!
            if (work == null){
                Console.WriteLine("My work here is done!");
                return;
            }
            work();
        }
    }

    public void start(){
        foreach (Thread t in threads){
            t.Start();
        }
    }

    public void shutdown() {
        foreach (Thread t in threads){
            t.Join();
            Console.WriteLine("Thread joined");
        }
    }
}

class Test{
    public static void Main(){
        ThrPool tpool = new ThrPool(5, 10);
        for (int i = 0; i < 5; i++){
            tpool.AssyncInvoke(new ThrWork(new A(i).DoWorkA));
            tpool.AssyncInvoke(new ThrWork(new B(i).DoWorkB));
        }

        tpool.start();
        tpool.shutdown();

        Console.ReadLine();
    }
}

class A{
    private int _id;

    public A(int id){
        _id = id;
    }

    public void DoWorkA(){
        Console.WriteLine("A-{0}", _id);
    }
}


class B{
    private int _id;

    public B(int id){
        _id = id;
    }

    public void DoWorkB(){
        Console.WriteLine("B-{0}", _id);
    }
}