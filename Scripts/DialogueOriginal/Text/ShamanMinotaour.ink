INCLUDE global.ink
VAR tempVariable = 0

~tempVariable = idiotPoints

{tempVariable >= 3: -> knot3 |-> main}

===main===
А вот и ты, духи видели твоё пришествие. Что-то мычит себе под нос... #speaker:Шаман #portrait:Minotaur

Мы не хотели причинять какого-либо вреда, однако нам нужно было сделать это ради выживания.
    *[Чем помочь?] Что нужно сделать для того чтобы получить артефакт назад? #speaker:Рыцарь #portrait:Player
        Духи сказали что только Dh’oine может убить Dh’oine. #speaker:Шаман #portrait:Minotaur
        Выполнишь для меня это задание и мы в расчете. Заодно посмотрим, как быстро после телепортации твоё сознание прояснится.
                -> knot2
                
    *[План] Какой у вас план? Я так полагаю пока я не решу вашу проблемму, назад вы мне его не отдадите. #speaker:Рыцарь #portrait:Player
            Нет никакого плана, ты просто убьёшь его и освободишь нас.  #speaker:Шаман #portrait:Minotaur
            -> knot2
        
===knot2===
 #speaker:Шаман #portrait:Minotaur
А теперь иди, я буду молиться за твою победу...

-> END

===knot3===
Большой муууу #speaker:Рыцарь #portrait:Player
Вижу ты не самого большого ума... Что-то мычит себе под нос... #speaker:Шаман #portrait:Minotaur
Как бы это правильно сказать...
Ты хочешь золотую блестяшку, чтобы её получить нужно сделать хорошее дело
Нужно убить очень плохого Dh’oine. Убивать конечно плохо, но ты сделаешь хорошо.
Надеюсь ты всё правильно понял.
А теперь иди и убей его
-> END