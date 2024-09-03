#if TOOLS
using Godot;
using System;

namespace Electronova
{
    [Tool]
    public partial class ElectronovaSystems : EditorPlugin
    {
	    public override void _EnterTree()
	    {
		    // Initialization of the plugin goes here.
            
	    }

	    public override void _ExitTree()
	    {
		    // Clean-up of the plugin goes here.
            
	    }
    }
}
#endif