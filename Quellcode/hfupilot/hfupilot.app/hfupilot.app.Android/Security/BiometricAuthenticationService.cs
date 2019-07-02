using Android.Content;
using Android.Hardware.Biometrics;
using Android.OS;
using Javax.Crypto;
using System;



namespace hfupilot.app.Droid.Security
{

    public class BiometricAuthenticationService : IBiometricAuthenticationService
    {
        public void Encrypt(byte[] input, Action<byte[]> success, Action<string> error)
        {
            var prompt = BuildPrompt();
            prompt.Authenticate(
                _cryptoHelper.CreateCryptoObject(CipherMode.EncryptMode),
                new CancellationSignal(), MainActivity.Activity.MainExecutor,
                new BiometricEncryptionCallback(input, success, error));
        }

        public void Decrypt(byte[] input, Action<byte[]> success, Action<string> error)
        {
            var prompt = BuildPrompt();
            prompt.Authenticate(
                _cryptoHelper.CreateCryptoObject(CipherMode.DecryptMode),
                new CancellationSignal(), MainActivity.Activity.MainExecutor,
                new BiometricEncryptionCallback(input, success, error));
        }

        private BiometricPrompt BuildPrompt()
        {
            return new BiometricPrompt.Builder(MainActivity.Activity)
                .SetDescription("Authenticate with Biometrics!")
                .SetTitle("TODO BIO")
                .SetNegativeButton("Cancel", MainActivity.Activity.MainExecutor, new CancelClickListener())
                .Build();
        }
        private class BiometricEncryptionCallback : BiometricPrompt.AuthenticationCallback
        {
            public BiometricEncryptionCallback(byte[] input, Action<byte[]> success, Action<string> error)
            {
                _input = input;
                _success = success;
                _error = error;
            }

            public override void OnAuthenticationSucceeded(BiometricPrompt.AuthenticationResult result)
            {
                if (BiometricCryptoHelper.IV == null)
                {
                    BiometricCryptoHelper.IV = result.CryptoObject.Cipher.GetIV();
                }

                _success(result.CryptoObject.Cipher.DoFinal(_input));
            }

            public override void OnAuthenticationFailed()
            {
                // NOP
            }

            //public override void OnAuthenticationError([GeneratedEnum] BiometricErrorCode errorCode, ICharSequence errString)
            //{
            //  _error(errString.ToString());
            //}

            private readonly byte[] _input;
            private readonly Action<byte[]> _success;
            private readonly Action<string> _error;
        }
        private class CancelClickListener : Java.Lang.Object, IDialogInterfaceOnClickListener
        {
            public void OnClick(IDialogInterface dialog, int which)
            {
                // NOP
            }
        }

        //private readonly BiometricCryptoHelper _cryptoHelper = new BiometricCryptoHelper();
        private readonly BiometricCryptoHelper _cryptoHelper = new BiometricCryptoHelper();
    }
}
