VAR gorbage = 4
-> intro 

===intro
#char Racoon
    Gimmie da gorbage 
    
    +[items] ->items
    +[exit] ->leave 

===items
#char Racoon
    Here's the some non-gorbage things 
    
    +[Food] ->thanks
    +[Water] ->thanks
    +[Return] ->intro

===checkGorabge
{   - gorbage > 0:
        ~gorbage = gorbage - 1
        ->thanks
    - else:
        ->noGorbage
}

===thanks
#char Racoon
    Thank you for your gorbage!
    ->items

===noGorbage
#char Racoon 
    No gorbage, No item!
    ->items
    
===leave
    Bring back more gorbage!
->DONE