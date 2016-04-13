using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CameraManager
{
    interface ICamera
    {
        void Configuration();

        void Grab();
        void SnapShot();
        void Stop();
        void Delete();
    }
}
