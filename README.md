# pseudocode-to-vhdl-compiler
The executable is located in the folder:
> assembly/bin/debug   
### Example pseudocode simple
```
input a;
input b

c = 3

if ( a > b )
a = b - 1
endif;

if a > 5
b = a + 4
else
b = a
endif

while(c > 0)
a = a + 1
c = c - 1
endwhile

output a;
output b

end;
```
### Example pseudocode medium

```
input a;
input b

if a = 1
  c = 1
elsif a = 2
  c = a * 2
elsif a = 3
  c = b * a
else
  a *= 2
endif

a += b
c = a + c

output c

end
```
