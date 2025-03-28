#alt+shift+c para parar o script

while exists(Pattern("button.png").similar(0.32)):
    
    time.sleep(1)

    #Data de produção
    click(Location(225, 165))
    keyDown(Key.SHIFT) 
    for _ in range(10): 
        type(Key.RIGHT)
    keyUp(Key.SHIFT) 
    type("c", Key.CTRL)
    dateProd = App.getClipboard()

    click(Location(1016, 330))
    type(dateProd)

    #Hora de produção
    click(Location(225, 219))
    keyDown(Key.SHIFT) 
    for _ in range(8): 
        type(Key.RIGHT)
    keyUp(Key.SHIFT) 
    type("c", Key.CTRL)
    HoraProd = App.getClipboard()

    click(Location(1015, 372))
    type(HoraProd)

    #Código peça
    click(Location(225, 273))
    keyDown(Key.SHIFT) 
    for _ in range(8): 
        type(Key.RIGHT)
    keyUp(Key.SHIFT) 
    type("c", Key.CTRL)
    CodPeca = App.getClipboard()

    click(Location(1016, 281))
    type(CodPeca)

    #tempo produção
    click(Location(225, 322))
    keyDown(Key.SHIFT) 
    for _ in range(8): 
        type(Key.RIGHT)
    keyUp(Key.SHIFT) 
    type("c", Key.CTRL)
    TempoProd = App.getClipboard()

    click(Location(1012, 416))
    type(TempoProd)

    #Inserir produto
    click(Location(946, 467))
    if exists(Pattern("Erro1.png").similar(0.72)):
       break

    #Resultado do teste
    click(Location(224, 372))
    keyDown(Key.SHIFT) 
    for _ in range(8): 
        type(Key.RIGHT)
    keyUp(Key.SHIFT) 
    type("c", Key.CTRL)
    ResultadoTeste = App.getClipboard()

    click(Location(1293, 328))
    type(ResultadoTeste)

    #Data do teste
    click(Location(224, 426))
    keyDown(Key.SHIFT) 
    for _ in range(8): 
        type(Key.RIGHT)
    keyUp(Key.SHIFT) 
    type("c", Key.CTRL)
    DataTeste = App.getClipboard()

    click(Location(1290, 378))
    type(DataTeste)
    
    #Inserir teste
    click(Location(1235, 468))
    if exists(Pattern("Erro1.png").similar(0.72)):
        break

    #Proxima peça
    Location(584, 487)
    