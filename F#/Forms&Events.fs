open System
open System.Windows.Forms
open System.Drawing


let togonImage = Image.FromFile("lmp1.jpg")
let togoffImage = Image.FromFile("lmp2.jpg")
let icons = new ImageList();
icons.Images.AddRange[|togonImage;togoffImage|]
icons.ImageSize <- new Size(40, 40);

let On (btn:Button)     = btn.ImageIndex <- 0
let Off (btn:Button)    = btn.ImageIndex <- 1

let buttonSize  = new Size(50,50)
let button2Size = new Size(30,20)
let winSize = new Size(20,90)
let formHeight = 700
let formWidth  = 700
let doorSize = new Size(20,84);

let buttonFactory top left = 
    let btn =  new Button(ImageList = icons,ImageIndex = 0, Size = buttonSize, Top = top,Left = left)
    btn.Click.AddHandler(fun _ _ -> btn.ImageIndex <- 1 - btn.ImageIndex)
    btn

let winFactory top left = 
    let win =  new Button(BackColor = Color.Red, Top = top, Left = left,Size = winSize)
    win.Click.AddHandler(fun _ _ -> if win.BackColor = Color.Green then win.BackColor <- Color.Red else win.BackColor <- Color.Green)
    win

let CtrlForm = 

    let form = new Form(Text = "Ctrlform", Height = formHeight, Width = formWidth) 
    form.BackgroundImage <- Image.FromFile("pic.jpg")           
    
    let btn1 = buttonFactory 50 577  
    let btn2 = buttonFactory 520 577
    let btn3 = buttonFactory 520 300  
    let btn4 = buttonFactory 50 400
    let btn5 = buttonFactory 200 250
    let btn6 = buttonFactory 310 577
  
    let lightbtns = [|btn1;btn2;btn3;btn4;btn5;btn6|]
    
    let btn7 = new Button(Top = 200,Left = 630, Size = button2Size)
    btn7.Click.AddHandler(fun _ _ -> Array.ForEach(lightbtns,Action<Button> Off))
    
    let lab1 = new Label(BackColor = Color.White, Text = "24С",Top = 100, Left = 100)
    
    let win1 = winFactory 400 110
    let win2 = winFactory 540 110
    
    let door = new Button(BackColor = Color.Green, Top = 225, Left = 675,Size = doorSize)    
    door.Click.AddHandler(fun _ _ -> if door.BackColor = Color.Green then door.BackColor <- Color.Red else door.BackColor <- Color.Green)
    
    let Entered _ _ = if door.BackColor = Color.Red then On btn6
    let EnterEventHandler = new EventHandler(Entered)   
    door.Click.AddHandler(EnterEventHandler)
    
    form.Controls.AddRange([|btn1;btn2;btn3;btn4;btn5;btn6;btn7;lab1;win1;win2;door|])
    form


do Application.Run(CtrlForm)
