// batov 271gr spbu. 2013.

module OO.Main
open System

type Product (name:string,cost:int,pack:string,place:int,amount:int) = 
    let mutable Amount = amount 
    member val Name = name with get, set
    member val Cost = cost with get, set
    member val Place = place with get, set 
    member val Pack = pack with get   
    member this.reduceAmount(count) =
      let d = amount-count 
      if (d >= 0) 
        then 
           Amount <- d
           true
        else 
           false
    
type FoodProduct (name:string,cost:int,pack:string,place:int,amount:int) = 
    inherit Product (name,cost,pack,place,amount)

   
type HouseholdProduct (name:string,cost:int,pack:string,place:int,amount:int) = 
    inherit Product (name,cost,pack,place,amount)

type Equipment (name:string,condition:int, cost:int) = 
    member val Name = name with get
    member val Condition = condition with get
    member val Cost = cost with get 
   
    
type CashBox (name:string,condition:int, cost:int) = 
    inherit Equipment (name,condition, cost)
    let mutable AmountOfMoney = 0
    member this.Cash = AmountOfMoney
    member this.PutCash(cash) = AmountOfMoney <- AmountOfMoney + cash
    member this.GetCash = 
       let t = AmountOfMoney 
       AmountOfMoney <- 0
       t
         
type Refrigerator (name:string,condition:int, cost:int) = 
    inherit Equipment (name,condition, cost)  
    let mutable temperature = 3
    member this.SetTemp(temp) = temperature <- temp
    member this.Temperature = temperature

type Staff (name:string, salary:int, standing:int) =
    member val Name = name with get, set
    member val Salary = salary with get, set
    member val Standing = standing with get, set 
   
type ProductionDepartment (name:string, salary:int, stanging:int, clothes:string) = 
    inherit Staff (name, salary, stanging)
    member val Clothes = clothes with get, set
    member this.sell(prod:Product,count,cashBox:CashBox) = 
     if prod.reduceAmount(count)
      then
        let t = prod.Cost
        cashBox.PutCash(t)
      
                                                                                                                                  
type FoodSeller (name:string, salary:int, stanging:int, clothes:string) =
    inherit ProductionDepartment (name, salary, stanging, clothes)
    
type HouseholdAppliancesSeller (name:string, salary:int, stanging:int, clothes:string) =
    inherit ProductionDepartment (name, salary, stanging, clothes)  

type Chiefs (name:string, salary:int, stanging:int) =
    inherit Staff (name, salary, stanging)
    
type Head (name:string, salary:int, stanging:int) =
    inherit Chiefs(name, salary, stanging)
    member this.SetSalary(man:Staff ,salary) = 
      man.Salary <- salary    
    
type Accountant (name:string, salary:int, stanging:int) =
    inherit Chiefs(name, salary, stanging)
    member this.TakeCash(cashBox:CashBox) = cashBox.GetCash
          
type idCard () =
    let mutable id = 1
    let mutable condition = 5
