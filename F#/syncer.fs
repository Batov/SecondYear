type Syncer(maxTasks : int) = 
    let  Queue = ref 1
    let MaxTasks = ref maxTasks
    member this.IsMyQueue taskNum = 
        taskNum = lock Queue (fun() -> !Queue)      
    member this.NextTask() =
        if !MaxTasks = lock Queue (fun() -> !Queue)
            then
                Queue := 0
                printfn "All Tasks completed"
            else
               lock Queue (fun() -> Queue := !Queue + 1)

let countOfTasks = 100
let syncer = new Syncer(countOfTasks)
let rand = new System.Random()

let task (num:int) = async {  
                        let time = rand.Next(1,1000)
                        do! Async.Sleep(time) //emulation of real work
                        while (syncer.IsMyQueue num <> true) do
                            ignore() //waiting 
                            do! Async.Sleep(5)
                        printfn "Task, Number = %A, Time = %A ms, finished!" num time
                        syncer.NextTask()
                     }

[for i in 1 .. countOfTasks -> task i]  |> Async.Parallel |> Async.RunSynchronously |> ignore

System.Console.Read() |> ignore
