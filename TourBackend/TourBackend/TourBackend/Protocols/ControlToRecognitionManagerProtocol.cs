using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proto;

namespace TourBackend
{
    /* 
     * The idea here is to define messageTypes for the communication between the actors.
     * We construct them always with a _pid = (_pid from the sender) such that we can always
     * have the information which actor has sent the message. And to identify the specific
     * message instance we also need a variable _messageID = _id (this is useful for 
     * debugging. 
     * In a second constructor we have the chance to give an additional target-string
     * where the message should go, meaning if the message should go over more than one 
     * actor level we can still specify the receiver of the message...
    */

    /* Now we first define all the tasks */

    
    public class StartVirtualObject
    {
        public PID senderPID;
        public string messageID;
        public string targetActor;

        public StartVirtualObject(string _messageID, PID _senderPID)
        {
            senderPID = _senderPID;
            messageID = _messageID;
        }

        public StartVirtualObject(string _messageID, PID _senderPID, string _targetActor)
        {
            senderPID = _senderPID;
            messageID = _messageID;
            targetActor = _targetActor;
        }
    }
    public class StopVirtualObject
    {
        public PID senderPID;
        public string messageID;
        public string targetActor;

        public StopVirtualObject(string _messageID, PID _senderPID)
        {
            senderPID = _senderPID;
            messageID = _messageID;
        }

        public StopVirtualObject(string _messageID, PID _senderPID, string _targetActor)
        {
            senderPID = _senderPID;
            messageID = _messageID;
            targetActor = _targetActor;
        }
    }
    public class SetActiveVirtualObject
    {
        public string messageID;
        public string toBeActiveVirtualObjectID;

        public SetActiveVirtualObject(string _messageID, string _toBeActiveVirtualObjectID)
        {
            messageID = _messageID;
            toBeActiveVirtualObjectID = _toBeActiveVirtualObjectID;
        }
    }
    public class SetInactiveVirtualObject
    {
        public PID senderPID;
        public string messageID;
        public string targetActor;

        public SetInactiveVirtualObject(string _messageID, PID _senderPID)
        {
            senderPID = _senderPID;
            messageID = _messageID;
        }

        public SetInactiveVirtualObject(string _messageID, PID _senderPID, string _targetActor)
        {
            senderPID = _senderPID;
            messageID = _messageID;
            targetActor = _targetActor;
        }
    }

    // here we need another variable time, meaning if it would take 
    // too long to get all the informations from all GameObjectActors
    // we can then easily define a behavior like throwing an error or sth else
    public class RequestAllVirtualObjects
    {
        public PID senderPID;
        public string messageID;
        public string targetActor;
        public TimeSpan time;

        public RequestAllVirtualObjects(string _messageID, PID _senderPID, TimeSpan _time)
        {
            senderPID = _senderPID;
            messageID = _messageID;
            time = _time;
        }

        public RequestAllVirtualObjects(string _messageID, PID _senderPID, string _targetActor, TimeSpan _time)
        {
            senderPID = _senderPID;
            messageID = _messageID;
            targetActor = _targetActor;
            time = _time;
        }
    }
    public class KillVirtualObject
    {
        public PID senderPID;
        public string messageID;
        public string targetActor;

        public KillVirtualObject(string _messageID, PID _senderPID)
        {
            senderPID = _senderPID;
            messageID = _messageID;
        }

        public KillVirtualObject(string _messageID, PID _senderPID, string _targetActor)
        {
            senderPID = _senderPID;
            messageID = _messageID;
            targetActor = _targetActor;
        }
    }

    /* Now we define all the responds to the upper tasks */

    
    public class RespondStartVirtualObject
    {
        public PID senderPID;
        public string messageID;
        public string targetActor;

        public RespondStartVirtualObject(string _messageID, PID _senderPID)
        {
            senderPID = _senderPID;
            messageID = _messageID;
        }

        public RespondStartVirtualObject(string _messageID, PID _senderPID, string _targetActor)
        {
            senderPID = _senderPID;
            messageID = _messageID;
            targetActor = _targetActor;
        }
    }
    public class RespondStopVirtualObject
    {
        public PID senderPID;
        public string messageID;
        public string targetActor;

        public RespondStopVirtualObject(string _messageID, PID _senderPID)
        {
            senderPID = _senderPID;
            messageID = _messageID;
        }

        public RespondStopVirtualObject(string _messageID, PID _senderPID, string _targetActor)
        {
            senderPID = _senderPID;
            messageID = _messageID;
            targetActor = _targetActor;
        }
    }
    public class RespondSetActiveVirtualObject
    {
        public string messageID;
        public string nowActiveVirtualObjectID;

        public RespondSetActiveVirtualObject(string _messageID, string _nowActiveVirtualObjectID)
        {
            messageID = _messageID;
            nowActiveVirtualObjectID = _nowActiveVirtualObjectID;
        }
    }
    public class RespondSetInactiveVirtualObject
    {
        public PID senderPID;
        public string messageID;
        public string targetActor;

        public RespondSetInactiveVirtualObject(string _messageID, PID _senderPID)
        {
            senderPID = _senderPID;
            messageID = _messageID;
        }

        public RespondSetInactiveVirtualObject(string _messageID, PID _senderPID, string _targetActor)
        {
            senderPID = _senderPID;
            messageID = _messageID;
            targetActor = _targetActor;
        }
    }
    // here we need another variable time, meaning if it would take 
    // too long to get all the informations from all GameObjectActors
    // we can then easily define a behavior like throwing an error or sth else
    // we also need a dictionary to be able to give all requested CodeObjects back to the ControlActor
    // in form of a dictionary with a key CodeObjectID and a value CodeObject itself
    public class RespondRequestAllVirtualObjects
    {
        public string messageID;
        public Dictionary<string, CodeObject> codeObjectIDToCodeObject;

        public RespondRequestAllVirtualObjects(string _messageID, Dictionary<string,CodeObject> _dict)
        {
            messageID = _messageID;
            codeObjectIDToCodeObject = _dict;
        }
    }
    public class RespondKillVirtualObject
    {
        public PID senderPID;
        public string messageID;
        public string targetActor;

        public RespondKillVirtualObject(string _messageID, PID _senderPID)
        {
            senderPID = _senderPID;
            messageID = _messageID;
        }

        public RespondKillVirtualObject(string _messageID, PID _senderPID, string _targetActor)
        {
            senderPID = _senderPID;
            messageID = _messageID;
            targetActor = _targetActor;
        }
    }

    /* Now we define all the fails to the upper tasks */

    
    public class FailedToStartVirtualObject
    {
        public PID senderPID;
        public string messageID;
        public string targetActor;

        public FailedToStartVirtualObject(string _messageID, PID _senderPID)
        {
            senderPID = _senderPID;
            messageID = _messageID;
        }

        public FailedToStartVirtualObject(string _messageID, PID _senderPID, string _targetActor)
        {
            senderPID = _senderPID;
            messageID = _messageID;
            targetActor = _targetActor;
        }
    }
    public class FailedToStopVirtualObject
    {
        public PID senderPID;
        public string messageID;
        public string targetActor;

        public FailedToStopVirtualObject(string _messageID, PID _senderPID)
        {
            senderPID = _senderPID;
            messageID = _messageID;
        }

        public FailedToStopVirtualObject(string _messageID, PID _senderPID, string _targetActor)
        {
            senderPID = _senderPID;
            messageID = _messageID;
            targetActor = _targetActor;
        }
    }
    public class FailedToSetActiveVirtualObject
    {
        public PID senderPID;
        public string messageID;
        public string targetActor;

        public FailedToSetActiveVirtualObject(string _messageID, PID _senderPID)
        {
            senderPID = _senderPID;
            messageID = _messageID;
        }

        public FailedToSetActiveVirtualObject(string _messageID, PID _senderPID, string _targetActor)
        {
            senderPID = _senderPID;
            messageID = _messageID;
            targetActor = _targetActor;
        }
    }
    public class FailedToSetInactiveVirtualObject
    {
        public PID senderPID;
        public string messageID;
        public string targetActor;

        public FailedToSetInactiveVirtualObject(string _messageID, PID _senderPID)
        {
            senderPID = _senderPID;
            messageID = _messageID;
        }

        public FailedToSetInactiveVirtualObject(string _messageID, PID _senderPID, string _targetActor)
        {
            senderPID = _senderPID;
            messageID = _messageID;
            targetActor = _targetActor;
        }
    }
    // here we need another variable time, meaning if it would take 
    // too long to get all the informations from all GameObjectActors
    // we can then easily define a behavior like throwing an error or sth else
    public class FailedToRequestAllVirtualObjects
    {
        public PID senderPID;
        public string messageID;
        public string targetActor;
        public TimeSpan time;

        public FailedToRequestAllVirtualObjects(string _messageID, PID _senderPID, TimeSpan _time)
        {
            senderPID = _senderPID;
            messageID = _messageID;
            time = _time;
        }

        public FailedToRequestAllVirtualObjects(string _messageID, PID _senderPID, string _targetActor, TimeSpan _time)
        {
            senderPID = _senderPID;
            messageID = _messageID;
            targetActor = _targetActor;
            time = _time;
        }
    }
    public class FailedToKillVirtualObject
    {
        public PID senderPID;
        public string messageID;
        public string targetActor;

        public FailedToKillVirtualObject(string _messageID, PID _senderPID)
        {
            senderPID = _senderPID;
            messageID = _messageID;
        }

        public FailedToKillVirtualObject(string _messageID, PID _senderPID, string _targetActor)
        {
            senderPID = _senderPID;
            messageID = _messageID;
            targetActor = _targetActor;
        }
    }

}
