using System.Globalization;

namespace KEpoch
{
    public partial class MainPage : ContentPage
    {
        long TZsec = 0;
        string TZstring = "+ 00:00";

        public MainPage()
        {
            InitializeComponent();
            string FullVersion = AppInfo.Current.VersionString;
            string[] arr = FullVersion.Split('.');
            string DisplayVersion = string.Join(".", arr.Take(3));
            VersionLabel.Text = "V" + DisplayVersion;

            GetLocalTimeZone();
        }

        private async void GitHub_Tapped(object sender, EventArgs e)
        {
            await Launcher.OpenAsync("https://github.com/kerwin-shen/kepoch");
        }

        private void GetLocalTimeZone()
        {
            /* get Local time */
            DateTimeOffset dt_local = DateTimeOffset.Now;
            DateTimeOffset dt_utc = DateTimeOffset.UtcNow;
            TZsec = (long)dt_local.Offset.TotalSeconds;

            if (dt_local.Offset < TimeSpan.Zero)
            {
                TZstring = "UTC - " + dt_local.Offset.ToString("hh\\:mm");
            }
            else
            {
                TZstring = "UTC + " + dt_local.Offset.ToString("hh\\:mm");
            }

            long utcsec_dec = dt_utc.ToUnixTimeSeconds();

            /* convert UTC seconds to date time */
            string utctime_str = DateTimeOffset.FromUnixTimeSeconds(utcsec_dec).UtcDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            string localtime_str = DateTimeOffset.FromUnixTimeSeconds(utcsec_dec + TZsec).UtcDateTime.ToString("yyyy-MM-dd HH:mm:ss");

            /* set text */
            txtZone.Text = TZstring;
            txtSecDec.Text = utcsec_dec.ToString();
            txtSecHex.Text = utcsec_dec.ToString("X8");
            txtUtcTime.Text = utctime_str;
            txtLocalTime.Text = localtime_str;
        }

        private void BtnCurTime_Click(object sender, EventArgs e)
        {
            GetLocalTimeZone();
        }

        private void BtnUTCSecondsDEC_Click(object sender, EventArgs e)
        {
            /*             0; UTC 1970-01-01 00:00:00; Beijing 1970-01-01 08:00:00.   */
            /*    2147483647; UTC 2038-1-19 03:14:07;  Beijing 2038-1-19 11:14:07.    */
            string utctime_str = "1970-01-01 00:00:00";
            string localtime_str = "1970-01-01 08:00:00";
            bool is_ok = false;

            /* get UTC seconds (DEC) */
            string utc_sec_str = txtSecDec.Text;

            /* check and convert */
            if (long.TryParse(utc_sec_str, out long utcsec_dec))
            {
                if ((utcsec_dec >= 0) && (utcsec_dec <= 2147483647))
                {
                    /* convert UTC seconds to date time */
                    utctime_str = DateTimeOffset.FromUnixTimeSeconds(utcsec_dec).UtcDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                    localtime_str = DateTimeOffset.FromUnixTimeSeconds(utcsec_dec + TZsec).UtcDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                    /* success */
                    is_ok = true;
                }
            }

            /* fails, set to 0 */
            if (false == is_ok)
            {
                utcsec_dec = 0;
            }

            /* set text */
            txtSecDec.Text = utcsec_dec.ToString();
            txtSecHex.Text = utcsec_dec.ToString("X8");
            txtUtcTime.Text = utctime_str;
            txtLocalTime.Text = localtime_str;
        }

        private void BtnUTCSecondsHEX_Click(object sender, EventArgs e)
        {
            /*             0; UTC 1970-01-01 00:00:00; Beijing 1970-01-01 08:00:00.   */
            /*    2147483647; UTC 2038-1-19 03:14:07;  Beijing 2038-1-19 11:14:07.    */
            string utctime_str = "1970-01-01 00:00:00";
            string localtime_str = "1970-01-01 08:00:00";
            bool is_ok = false;

            /* get UTC seconds (HEX) */
            string utc_sec_hexstr = txtSecHex.Text;
            string utc_sec_str = utc_sec_hexstr.Replace(" ", ""); //remove space

            /* check and convert */
            if (long.TryParse(utc_sec_str, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out long utcsec_dec))
            {
                if ((utcsec_dec >= 0) && (utcsec_dec <= 2147483647))
                {
                    /* convert UTC seconds to date time */
                    utctime_str = DateTimeOffset.FromUnixTimeSeconds(utcsec_dec).UtcDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                    localtime_str = DateTimeOffset.FromUnixTimeSeconds(utcsec_dec + TZsec).UtcDateTime.ToString("yyyy-MM-dd HH:mm:ss");

                    /* success */
                    is_ok = true;
                }
            }

            /* fails, set to 0 */
            if (false == is_ok)
            {
                utcsec_dec = 0;
            }

            /* set text */
            txtSecDec.Text = utcsec_dec.ToString();
            txtSecHex.Text = utcsec_dec.ToString("X8");
            txtUtcTime.Text = utctime_str;
            txtLocalTime.Text = localtime_str;
        }

        private void BtnUTCTime_Click(object sender, EventArgs e)
        {
            /*             0; UTC 1970-01-01 00:00:00; Beijing 1970-01-01 08:00:00.   */
            /*    2147483647; UTC 2038-1-19 03:14:07;  Beijing 2038-1-19 11:14:07.    */
            string utctime_str = "1970-01-01 00:00:00";
            string localtime_str = "1970-01-01 08:00:00";
            bool is_ok = false;
            long utcsec_dec = 0;

            /* get UTC time */
            string time_str = txtUtcTime.Text;

            /* check and convert */
            if (DateTime.TryParseExact(time_str, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dt))
            {
                DateTimeOffset dt_utc = new DateTimeOffset(dt, TimeSpan.Zero);
                utcsec_dec = dt_utc.ToUnixTimeSeconds();

                if ((utcsec_dec >= 0) && (utcsec_dec <= 2147483647))
                {
                    /* convert UTC seconds to date time */
                    utctime_str = DateTimeOffset.FromUnixTimeSeconds(utcsec_dec).UtcDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                    localtime_str = DateTimeOffset.FromUnixTimeSeconds(utcsec_dec + TZsec).UtcDateTime.ToString("yyyy-MM-dd HH:mm:ss");

                    /* success */
                    is_ok = true;
                }
            }

            /* fails, set to 0 */
            if (false == is_ok)
            {
                utcsec_dec = 0;
            }

            /* set text */
            txtSecDec.Text = utcsec_dec.ToString();
            txtSecHex.Text = utcsec_dec.ToString("X8");
            txtUtcTime.Text = utctime_str;
            txtLocalTime.Text = localtime_str;
        }

        private void BtnLocalTime_Click(object sender, EventArgs e)
        {
            /*             0; UTC 1970-01-01 00:00:00; Beijing 1970-01-01 08:00:00.   */
            /*    2147483647; UTC 2038-1-19 03:14:07;  Beijing 2038-1-19 11:14:07.    */
            string utctime_str = "1970-01-01 00:00:00";
            string localtime_str = "1970-01-01 08:00:00";
            bool is_ok = false;
            long utcsec_dec = 0;

            /* get Local time */
            string time_str = txtLocalTime.Text;

            /* check and convert */
            if (DateTime.TryParseExact(time_str, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dt))
            {
                DateTimeOffset dt_sec = new DateTimeOffset(dt, TimeSpan.Zero);
                utcsec_dec = dt_sec.ToUnixTimeSeconds() - TZsec;

                if ((utcsec_dec >= 0) && (utcsec_dec <= 2147483647))
                {
                    /* convert UTC seconds to date time */
                    utctime_str = DateTimeOffset.FromUnixTimeSeconds(utcsec_dec).UtcDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                    localtime_str = DateTimeOffset.FromUnixTimeSeconds(utcsec_dec + TZsec).UtcDateTime.ToString("yyyy-MM-dd HH:mm:ss");

                    /* success */
                    is_ok = true;
                }
            }

            /* fails, set to 0 */
            if (false == is_ok)
            {
                utcsec_dec = 0;
            }

            /* set text */
            txtSecDec.Text = utcsec_dec.ToString();
            txtSecHex.Text = utcsec_dec.ToString("X8");
            txtUtcTime.Text = utctime_str;
            txtLocalTime.Text = localtime_str;
        }
    }
}
