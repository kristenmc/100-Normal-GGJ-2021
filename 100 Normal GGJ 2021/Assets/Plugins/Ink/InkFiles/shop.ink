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
        +[Food - 1] ->food 
        +[Water - 1] ->water
        +[Water Purifier - 1] ->water_purifier
        +[Net - 1] -> net
        +[Metal Detector - 1] ->metal_detector
    -else:
         -> noGorbage
    }
    +[Return] ->intro

==food==
#food
You gained some food! 
    +[Yay!] ->thanks

==water==
#water
You gained some water! 
    +[Yay!] ->thanks


==water_purifier==
#water_purifier
You gained a water purifier, increasing your water yield!
        +[Yay!] ->thanks

==net==
#net
You gained a net, increasing your food yield!
    +[Yay!] ->thanks

==metal_detector==
#metal_detector
you gained a metal detector, increasing your gorbage yield!
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