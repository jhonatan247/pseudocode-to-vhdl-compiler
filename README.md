# From pseudocode to vhdl compiler
The executable is located in the folder:
> assembly/bin/debug   
### Example pseudocode easy
```
input a;
input b

if ( a > b )
a = b - 1
endif;

if a >= 5
b = a + 4
else
b = a
endif

c = 3

while(c > 0)
a = a + 1
c = c - 1
endwhile

output a;
output b

end;
```
### Mid-level pseudocode example 

```
input a;
input b

// comment
while a > 0
  if a = 1
    c = 1
  elsif a = 2
    c = a * 2
  elsif a = 3
    c = b * a
  else
    b *= 2
  endif
  a -= 1;
endwhile

a += b
c = a + c

output c

end
```
