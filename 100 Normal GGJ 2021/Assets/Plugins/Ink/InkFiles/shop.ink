VAR haveGorbage = true
-> enter 
===intro==
#check_gorbage
    {~Gimmie da gorbage!| Got gorbage?!|Is that gorbage?!| Smells like gorbage!} 
    
    +[items] ->items
    +[exit] ->leave 

===items==
{-checkTrue(haveGorbage):
        {~non-gorbage things!|Things you want?|Let's trade!} 
        +[Food and Water - 1] ->food 
        +[Net - 1] -> net
        +[Metal Detector - 1] ->metal_detector
    -else:
         -> noGorbage
    }
    +[Return] ->intro

==food==
#food
Here's some food and water! 
    +[Yay!] ->thanks

==water_purifier==
#water_purifier
You gained a water purifier, it increasing your water yield!
        +[Yay!] ->thanks

==net==
#net
Here's a net, it increases your resource yield!
    +[Yay!] ->thanks

==metal_detector==
#metal_detector
Here's a metal detector, it increases your gorbage yield!
    +[Yay!] ->thanks


===thanks==
#check_gorbage
    {~Thank you for your gorbage!|Your gorbage now belongs to me|Gorbage hehe UwU}
    +[Return]->items

===noGorbage==
    {~No gorbage, No item!|Sad, I wanted gorbage ;u;| Go get more gorbage!}
    +[Return]->intro
    +[Exit] -> leave

===enter==
Welcome to Trash Pandas!
+[Enter] ->intro

===leave==
    {~Bring back more gorbage!|Byebye gorbage bear!|Come back soon!}
    +[Bye!]->bye

===bye==
#end
->enter

=== function checkTrue(bool) ===
    ~return bool == true 