VG_PR_Procdump_Monitor
by SemlerPDX July2024

When procdump creates a crash dump, it must be restarted, and this app
does this by monitoring the state of the PR Server itself.  This means a 
false positive could occur if any admin with TCAdmin access needed to 
stop and then start (i.e. restart) the PR Server manually.

When VG_PR_Procdump_Monitor is launched, procdump will attach itself to 
an already running instance of PR Server, any may even supercede TCAdmin 
and restart it following a crash faster than TCAdmin can - this causes no 
trouble to TCAdmin or my app, a second instance of the same server is not 
possible and a race condition would be handled internally by TCAdmin.

Dump files are LARGE and so this program needs daily oversight when run, 
to monitor for crash dump files and to remove them from the server for
review or transfer to R-DEVs.  Dump files should appear in the same 
folder as the procdump executable that this application monitors.

Right-click on dump files and use the WinRAR shortcut "Best RAR and Test",
or the "Compress and Test ZIP" option - I created these as timesavers just
for dump files like this, and they can go from 2GB+ down to 200-500MB at most.

Any issues, contact me.

-Sem
