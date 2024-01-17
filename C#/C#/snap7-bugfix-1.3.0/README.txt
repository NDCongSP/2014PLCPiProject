********************************************************************************************
This is a daily bugfix package, you need to download first the full release Snap7-full.1.3.0
********************************************************************************************

To update your package overwite/merge the folders with the same name.

If you "absolutely" need a binary package for other platforms and you are not able
to rebuild Snap7 yourself and you cannot wait for the next official release send 
me an email.

[Fixed]

--------------------------------------------------------------------------------------------
Snap7.net.cs - S7Server
--------------------------------------------------------------------------------------------
When you share a memory area via RegisterArea the Garbage Collector doesn't know that the 
area is accessed by unmanaged code, so it feels free to move it across the memory.
The shared memory must be pinned.
I used GCHandle.Alloc() to create an handle for the area into RegisterArea function.
A dictionary keeps trak of areas wich are locked and unlocked trasparently to your program.

Thanks to Marco Masenello for reporting it and the tests made.

--------------------------------------------------------------------------------------------
snap7.pas - S7 Helper class
--------------------------------------------------------------------------------------------

The class was tested with Lazarus/RAD Studio XE5. Unluckily Legacy releases (D6/D7..)
don't have static classes support. 

Sorry I forgot it :(

I changed it to "normal" class, it's created into initialization and destroyed into
finalization.

Thanks to Rade Nisevic for reporting it.

--------------------------------------------------------------------------------------------
snap_threads.cpp
--------------------------------------------------------------------------------------------

The Windows thread class didn't release its thread handle on destruction.
This caused a memory leak of 4 or 8 bytes for each client destroyed.
Thanks to Andreas Bahns for reporting it.

--------------------------------------------------------------------------------------------

[Added]

--------------------------------------------------------------------------------------------
Snap7.net.cs - S7 Helper class
--------------------------------------------------------------------------------------------

Added GetS1200_DTLAt() and SetS1200_DTLAt() to read/write S7 1200/1500 DTL (DateTime) vars.
Thanks to Johan Cardoen that made them.

