//Batov Nikita, SPBU, Math, 2013.

type Syncer(maxTasks : int) = 
    let Queue    = ref 1 //First task
    let MaxTasks = ref maxTasks

    let safeReadQueue = lock Queue (fun() -> !Queue)
    let safeIncQueue  =  lock Queue (fun() -> Queue := !Queue + 1)

    member this.IsMyTurn taskNum = taskNum = safeReadQueue  // Check task's turn   
    member this.NextTask() = 
        if !MaxTasks = safeReadQueue
            then
                Queue := 0
                printfn "All Tasks completed"
            else
                safeIncQueue

let countOfTasks = 100
let syncer = new Syncer(countOfTasks)
let rand = new System.Random()

let task (num:int) = async {  
                        let time = rand.Next(1,1000)
                        do! Async.Sleep(time) //emulation of real work
                        while (syncer.IsMyTurn num <> true) do
                            ignore() //waiting 
                            do! Async.Sleep(5)
                        printfn "Task, Number = %A, Time = %A ms, finished!" num time
                        syncer.NextTask()
                     }

[for i in 1 .. countOfTasks -> task i]  |> Async.Parallel |>  ignore

System.Console.Read() |> ignore
