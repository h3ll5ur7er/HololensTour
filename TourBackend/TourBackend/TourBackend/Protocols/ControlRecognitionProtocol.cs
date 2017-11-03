using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proto;

namespace TourBackend
{
    public class StartVirtualObject {
        public PID pid;
        public string id;

        public StartVirtualObject(string _id, PID _pid) {
            pid = _pid;
            id = _id;
        }
    }
    public class StopVirtualObject {
        public PID pid;
        public string id;

        public StopVirtualObject(string _id, PID _pid)
        {
            pid = _pid;
            id = _id;
        }
    }
    public class SetActiveVirtualObject {
        public PID pid;
        public string id;

        public SetActiveVirtualObject(string _id, PID _pid)
        {
            pid = _pid;
            id = _id;
        }
    }
    public class SetInactiveVirtualObject {
        public PID pid;
        public string id;

        public SetInactiveVirtualObject(string _id, PID _pid)
        {
            pid = _pid;
            id = _id;
        }
    }
    public class KillVirtualObject {
        public PID pid;
        public string id;

        public KillVirtualObject(string _id, PID _pid)
        {
            pid = _pid;
            id = _id;
        }
    }

    public class RespondStartVirtualObject {
        public PID pid;
        public string id;

        public RespondStartVirtualObject(string _id, PID _pid)
        {
            pid = _pid;
            id = _id;
        }
    }
    public class RespondStopVirtualObject {
        public PID pid;
        public string id;

        public RespondStopVirtualObject(string _id, PID _pid)
        {
            pid = _pid;
            id = _id;
        }
    }
    public class RespondSetActiveVirtualObject {
        public PID pid;
        public string id;

        public RespondSetActiveVirtualObject(string _id, PID _pid)
        {
            pid = _pid;
            id = _id;
        }
    }
    public class RespondSetInactiveVirtualObject {
        public PID pid;
        public string id;

        public RespondSetInactiveVirtualObject(string _id, PID _pid)
        {
            pid = _pid;
            id = _id;
        }
    }
    public class RespondKillVirtualObject {
        public PID pid;
        public string id;

        public RespondKillVirtualObject(string _id, PID _pid)
        {
            pid = _pid;
            id = _id;
        }
    }

    public class FailedToStartVirtualObject {
        public PID pid;
        public string id;

        public FailedToStartVirtualObject(string _id, PID _pid)
        {
            pid = _pid;
            id = _id;
        }
    }
    public class FailedToStopVirtualObject {
        public PID pid;
        public string id;

        public FailedToStopVirtualObject(string _id, PID _pid)
        {
            pid = _pid;
            id = _id;
        }
    }
    public class FailedToSetActiveVirtualObject {
        public PID pid;
        public string id;

        public FailedToSetActiveVirtualObject(string _id, PID _pid)
        {
            pid = _pid;
            id = _id;
        }
    }
    public class FailedToSetInactiveVirtualObject {
        public PID pid;
        public string id;

        public FailedToSetInactiveVirtualObject(string _id, PID _pid)
        {
            pid = _pid;
            id = _id;
        }
    }
    public class FailedToKillVirtualObject {
        public PID pid;
        public string id;

        public FailedToKillVirtualObject(string _id, PID _pid)
        {
            pid = _pid;
            id = _id;
        }
    }

}
