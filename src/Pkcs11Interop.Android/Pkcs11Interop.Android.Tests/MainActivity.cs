using Android.App;
using Android.OS;
using System.Reflection;
using Xamarin.Android.NUnitLite;

namespace Pkcs11Interop.Android.Tests
{
    [Activity(Label = "Pkcs11Interop.Android.Tests", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : TestSuiteActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            AddTest(Assembly.GetExecutingAssembly());
            base.OnCreate(bundle);
        }
    }
}

