using System;
using System.Collections.Generic;
using System.Text;

namespace hfupilot.app
{
    public interface IBiometricAuthenticationService
    {
        void Authenticate(Action success, Action<string> onError);
    }
}
