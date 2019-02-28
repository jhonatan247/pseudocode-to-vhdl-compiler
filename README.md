# pseudocode-to-vhdl-compiler
The executable is located in the folder:
> assembly/bin/debug
###Example pseudocode
´´´
input a;
input b

c = 3

onlyif ( a > b )
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
´´´
