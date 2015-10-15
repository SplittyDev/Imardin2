# Imardin2
Imardin2 is a virtual machine with its own assembly language.  
It contains an assembler (imc), the vm itself (imvm) and a library for shared code.

# imc
imc is the imardin assembler.  
It takes an assembly source file as input and spits out bytecode targetting the imardin vm.

An example assembly file may look like the following:
```asm
jmp main

main:
  mov %eax, 0x10 # move 0x10 into the eax register
```

Command-line arguments and switches:

| Argument | Description
| -------- | -----------
| -i       | Input source
| -o       | Output binary

# imvm
imvm is the imardin vm.  
It takes compiled imardin bytecode as input and interprets it.

Command-line arguments and switches:

| Argument       | Description
| -------------- | -----------
| -i             | Input binary
| -m<br>--memory | Memory size
| -s<br>--stack  |  Stack address

Please be aware that the stack grows to the top,  
so be sure to allocate enough space for the stack.  
You can also use . as stack address to place it at the end of the memory.

# libImardin2
libImardin2 is a dynamic link library that contains most of the code that  
imc and the imvm use.
