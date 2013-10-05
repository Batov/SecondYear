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

let CtrlForm = 

    let form = new Form(Text = "Ctrlform", Height =700, Width = 700) 
    form.BackgroundImage <- Image.FromFile("pic.jpg")           
    
    let btn1 = new Button(ImageList = icons,ImageIndex = 0,Size = new Size(50,50),Top = 50,Left = 577)
    btn1.Click.AddHandler(fun _ _ -> btn1.ImageIndex <- 1 - btn1.ImageIndex)
    
    let btn2 = new Button(ImageList = icons,ImageIndex = 0,Size = new Size(50,50),Top = 520,Left = 577)
    btn2.Click.AddHandler(fun _ _ -> btn2.ImageIndex <- 1 - btn2.ImageIndex)
    
    let btn3 = new Button(ImageList = icons,ImageIndex = 0,Size = new Size(50,50),Top = 520,Left = 300)
    btn3.Click.AddHandler(fun _ _ -> btn3.ImageIndex <- 1 - btn3.ImageIndex)
    
    let btn4 = new Button(ImageList = icons,ImageIndex = 0,Size = new Size(50,50),Top = 50,Left = 400)
    btn4.Click.AddHandler(fun _ _ -> btn4.ImageIndex <- 1 - btn4.ImageIndex)
    
    let btn5 = new Button(ImageList = icons,ImageIndex = 0,Size = new Size(50,50),Top = 200,Left = 250)
    btn5.Click.AddHandler(fun _ _ -> btn5.ImageIndex <- 1 - btn5.ImageIndex)
    
    let btn6 = new Button(ImageList = icons,ImageIndex = 0,Size = new Size(50,50),Top = 310,Left = 577)
    btn6.Click.AddHandler(fun _ _ -> btn6.ImageIndex <- 1 - btn6.ImageIndex)
    
    let lightbtns = [|btn1;btn2;btn3;btn4;btn5;btn6|]
    
    let btn7 = new Button(Top = 200,Left = 630, Size = new Size(30,20))
    btn7.Click.AddHandler(fun _ _ -> Array.ForEach(lightbtns,Action<Button> Off))
    
    let lab1 = new Label(BackColor = Color.White, Text = "24С",Top = 100, Left = 100)
    
    let win1 = new Button(BackColor = Color.Red, Top = 400, Left = 110,Size = new Size(20,90))
    win1.Click.AddHandler(fun _ _ -> if win1.BackColor = Color.Green then win1.BackColor <- Color.Red else win1.BackColor <- Color.Green)
    let win2 = new Button(BackColor = Color.Red, Top = 540, Left = 110,Size = new Size(20,90))
    win2.Click.AddHandler(fun _ _ -> if win2.BackColor = Color.Green then win2.BackColor <- Color.Red else win2.BackColor <- Color.Green) 
    
    let door = new Button(BackColor = Color.Green, Top = 225, Left = 675,Size = new Size(20,84))    
    door.Click.AddHandler(fun _ _ -> if door.BackColor = Color.Green then door.BackColor <- Color.Red else door.BackColor <- Color.Green)
    
    let Entered _ _ = if door.BackColor = Color.Red then On btn6
    let EnterEventHandler = new EventHandler(Entered)   
    door.Click.AddHandler(EnterEventHandler)
    
    form.Controls.AddRange([|btn1;btn2;btn3;btn4;btn5;btn6;btn7;lab1;win1;win2;door|])
    form


do Application.Run(CtrlForm)
