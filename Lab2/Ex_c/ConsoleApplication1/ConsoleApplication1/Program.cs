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

    private Queue<ThrWork> queue;
    private List<Thread> threads;
    private int queueCapacity = 0;

    public ThrPool(int thrNum, int bufSize){

        queue = new Queue<ThrWork>(bufSize);
        threads = new List<Thread>();
        queueCapacity = bufSize;
        
        // initialize
        for (int i = 0; i < thrNum; i++) {
            threads.Add(new Thread(threadLoop));
        }
    }

    public void AssyncInvoke(ThrWork action)
    {
        lock (queue)
        {
            while (queueCapacity == queue.Count)
            {
                System.Console.WriteLine("(queueCapacity == queue.Count)" + queue.Count);

                Monitor.Wait(queue);
            }

            queue.Enqueue(action);

            //every thread is waiting
            if (queue.Count == 1)
            {
                //so notify only one thread that is able to work
                Monitor.Pulse(queue);
            }
        }
    }

    public void AssyncInvoke2(ThrWork action){
        //Thread t = new Thread(this.AssyncInvoke);
        //t.Start(action);
    }

    private void threadLoop(){
        while (true) { 
            lock (queue) {

                while (queue.Count == 0) //waiting for work
                {
                    System.Console.WriteLine("(queue.Count == 0)");
                    Monitor.Wait(queue);
                }

                ThrWork work = queue.Dequeue();

                if (queue.Count == queueCapacity - 1) //a produced is waiting for a free space
                {
                    Monitor.Pulse(queue);
                }
                work();
            }
        }
    }

    public void start(){
        foreach (Thread t in threads){
            t.Start();
        }
    }
}

class Test{

    ThrPool tpool = new ThrPool(4, 10);

    
    public Test(ThrPool t)
    {
        tpool = t;
    }


    public static void Main(){

        Test t = new Test(new ThrPool(4, 10));
        
        t.tpool.start();

        for (int i = 0; i < 10; i++)
        {
            t.tpool.AssyncInvoke(new ThrWork(new A(i).DoWorkA));
            t.tpool.AssyncInvoke(new ThrWork(new B(i).DoWorkB));
        }

        
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