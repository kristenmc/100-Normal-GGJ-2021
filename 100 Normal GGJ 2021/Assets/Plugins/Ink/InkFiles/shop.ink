VAR haveGorbage = true
-> intro 
===intro==
#check_gorbage
    Gimmie da gorbage 
    
    +[items] ->items
    +[exit] ->leave 

===items==
{-checkTrue(haveGorbage):
        Here's the some non-gorbage things 
        +[Food] ->food 
        +[Water] ->water
    -else:
         -> noGorbage
    }
    +[Return] ->intro

==food==
#food
You gained some food! 
    +[yay!] ->thanks

==water==
#water
You gained some water! 
    +[yay!] ->thanks
===thanks==
#check_gorbage
    Thank you for your gorbage!
    +[Return]->items

===noGorbage==
    No gorbage, No item!
    +[Return]->intro
    +[Exit] -> leave
    
===leave==
    Bring back more gorbage!
    +[Bye!]->exit

===exit==
#end
->DONE

=== function checkTrue(bool) ===
    ~return bool == true 