INCLUDE global.ink

-> main

===main===

Рыцарь произошло несчастье #speaker:Король #portrait:King
Мой король что случилось? #speaker:Рыцарь #portrait:Player
Из королевской сокровищницы украли древний артефакт, дарующий смертным силу богов, а бессмертных превращает в них#speaker:Король #portrait:King

    *Где они? #speaker:Рыцарь #portrait:Player
      Ушли через старые катакомбы оставив дыру в темнице #speaker:Король #portrait:King
      ->Knot2

    *Ы? в каво? #speaker:Рыцарь #portrait:Player
        ~idiotPoints += 1
      В богов! Ты меня вообще слушаешь? #speaker:Король #portrait:King
      ->Knot2

===Knot2===
Я даже не имею представление при помощи чего возможно было создать такую щель... #speaker:Король #portrait:King
Ты должен будешь пойти по их следу, надеюсь они ещё там
В катакомбах опасно, поэтому нужно быть осторожным
    *Хорошо #speaker:Рыцарь #portrait:Player
        Ступай же рыцарь, пусть твоё храброе сердце пылает огнем, а меч рубит без устали #speaker:Король #portrait:King
        ->Knot3
    *Щщель? #speaker:Рыцарь #portrait:Player
        ~idiotPoints += 1
        ИДИ УЖЕ...Болван #speaker:Король #portrait:King
        ->Knot3
        
===Knot3===
        
~trigger(0)
СТРАЖА ОТКРОЙТЕ ВХОД В ТЕМНИЦУ! #speaker:Король #portrait:King

-> END
