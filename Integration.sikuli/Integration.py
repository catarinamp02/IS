#alt+shift+c para parar o script

while exists(Pattern("button.png").similar(0.32)):
    
    time.sleep(1)

    #Data de produção
    click(Pattern("1743289897957.png").similar(0.88).targetOffset(84,6))
    keyDown(Key.SHIFT) 
    for _ in range(10): 
        type(Key.RIGHT)
    keyUp(Key.SHIFT) 
    type("c", Key.CTRL)
    dateProd = App.getClipboard()

    click(Pattern("1743291206273.png").similar(0.81).targetOffset(72,-4))
    type(dateProd)

    #Hora de produção
    click(Pattern("1743327881103.png").similar(0.90).targetOffset(85,4))
    keyDown(Key.SHIFT) 
    for _ in range(8): 
        type(Key.RIGHT)
    keyUp(Key.SHIFT) 
    type("c", Key.CTRL)
    HoraProd = App.getClipboard()

    click(Pattern("1743327917341.png").similar(0.80).targetOffset(76,-10))
    type(HoraProd)

    #Código peça
    click(Pattern("1743327940097.png").similar(0.86).targetOffset(85,-1))
    keyDown(Key.SHIFT) 
    for _ in range(8): 
        type(Key.RIGHT)
    keyUp(Key.SHIFT) 
    type("c", Key.CTRL)
    CodPeca = App.getClipboard()

    click(Pattern("1743327963837.png").similar(0.89).targetOffset(77,-8))
    type(CodPeca)

    #tempo produção
    click(Pattern("1743328041561.png").similar(0.84).targetOffset(77,0))
    keyDown(Key.SHIFT) 
    for _ in range(8): 
        type(Key.RIGHT)
    keyUp(Key.SHIFT) 
    type("c", Key.CTRL)
    TempoProd = App.getClipboard()

    click(Pattern("1743328067746.png").similar(0.89).targetOffset(68,-5))
    type(TempoProd)

    #Inserir produto
    click(Pattern("1743328100194.png").similar(0.77))
    sleep(2)
    if exists(Pattern("Erro1.png").similar(0.76)):
       break
    if exists("1743328691553.png"):
        break

    #Resultado do teste
    click(Pattern("1743328128381.png").similar(0.88).targetOffset(78,0))
    keyDown(Key.SHIFT) 
    for _ in range(8): 
        type(Key.RIGHT)
    keyUp(Key.SHIFT) 
    type("c", Key.CTRL)
    ResultadoTeste = App.getClipboard()

    click(Pattern("1743328166092.png").similar(0.87).targetOffset(68,-8))
    type(ResultadoTeste)

    #Data do teste
    click(Pattern("1743328197995.png").similar(0.87).targetOffset(89,0))
    keyDown(Key.SHIFT) 
    for _ in range(10): 
        type(Key.RIGHT)
    keyUp(Key.SHIFT) 
    type("c", Key.CTRL)
    DataTeste = App.getClipboard()

    click(Pattern("1743328226235.png").similar(0.86).targetOffset(74,-4))
    type(DataTeste)
    
    #Inserir teste
    click(Pattern("1743328249906.png").similar(0.84))
    sleep(2)
    if exists(Pattern("Erro1.png").similar(0.72)):
        break
    if exists("1743328691553.png"):
        break

    #Proxima peça
    click(Pattern("1743328278855.png").similar(0.80))
    