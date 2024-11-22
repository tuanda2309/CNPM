using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace _054205001268_SeleniumAutoTest
{
    public class _054205001268_SeleniumAutoTest
    {
        IWebDriver driver;
        private WebDriverWait wait;
        [SetUp]
        public void SetupTest()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void Test()
        {
            //dang ky
            driver.Navigate().GoToUrl("http://localhost:5297/Account/Register");
            Thread.Sleep(1000);

            driver.FindElement(By.Name("Username")).Clear();
            driver.FindElement(By.Name("Username")).SendKeys("Test");
            Thread.Sleep(1000);

            driver.FindElement(By.Name("Email")).Clear();
            driver.FindElement(By.Name("Email")).SendKeys("Test@gmail.com");
            Thread.Sleep(1000);

            driver.FindElement(By.Name("Password")).Clear();
            driver.FindElement(By.Name("Password")).SendKeys("123456");
            Thread.Sleep(1000);

            driver.FindElement(By.Name("ConfirmPassword")).Clear();
            driver.FindElement(By.Name("ConfirmPassword")).SendKeys("123456");
            Thread.Sleep(1000);
            var buttonRegister = driver.FindElement(By.CssSelector("button.submitBtn"));
            buttonRegister.Click();
            Thread.Sleep(2000);
            driver.Navigate().GoToUrl("http://localhost:5297/Account/Login");
            //

            // dang nhap
            driver.Navigate().GoToUrl("http://localhost:5297/Account/Login");

            Thread.Sleep(1000);

            driver.FindElement(By.Name("email")).Clear();
            driver.FindElement(By.Name("email")).SendKeys("dangkhoa18205@gmail.com");
            Thread.Sleep(1000);

            driver.FindElement(By.Id("password")).Clear();
            driver.FindElement(By.Id("password")).SendKeys("123456");
            Thread.Sleep(1000);
            var buttonLogin = driver.FindElement(By.CssSelector("button.submitBtn"));
            buttonLogin.Click();

            Thread.Sleep(2000);
            driver.Navigate().GoToUrl("http://localhost:5297/");
            //


            //dang nhap user : Staff
            driver.Navigate().GoToUrl("http://localhost:5297/Account/Login");

            Thread.Sleep(1000);

            driver.FindElement(By.Name("email")).Clear();
            driver.FindElement(By.Name("email")).SendKeys("khoast@gmail.com");
            Thread.Sleep(1000);

            driver.FindElement(By.Id("password")).Clear();
            driver.FindElement(By.Id("password")).SendKeys("123456");
            Thread.Sleep(1000);
            var button = driver.FindElement(By.CssSelector("button.submitBtn"));
            button.Click();

            Thread.Sleep(2000);
            driver.Navigate().GoToUrl("http://localhost:5297/");


            // dang phong
            driver.Navigate().GoToUrl("http://localhost:5297/Room/CreateRoom");
            Thread.Sleep(1000);
            driver.FindElement(By.Name("Name")).Clear();
            driver.FindElement(By.Name("Name")).SendKeys("Phòng Giá Rẻ");
            Thread.Sleep(1000);
            driver.FindElement(By.Name("OwnerName")).Clear();
            driver.FindElement(By.Name("OwnerName")).SendKeys("Khoa");
            Thread.Sleep(1000);
            string todayDate = DateTime.Now.ToString("dd/MM/yyyy");
            DateTime parsedDate = DateTime.ParseExact(todayDate, "dd/MM/yyyy", null);
            string formattedDate = parsedDate.ToString("yyyy-MM-dd");

            IWebElement dateInput = driver.FindElement(By.XPath("//input[@type='date']"));
            ((IJavaScriptExecutor)driver).ExecuteScript($"arguments[0].value = '{formattedDate}';", dateInput);
            Thread.Sleep(1000);
            driver.FindElement(By.Name("ImageFile")).Clear();
            driver.FindElement(By.Name("ImageFile")).SendKeys(@"D:\code\cnpm\PODBookingSystem\wwwroot\img\0a52fdf4-139a-41ea-9a69-dcd04b779fbc_777bf8fa-5961-4f97-97ec-f5218e367432_p28.jpg");
            Thread.Sleep(1000);
            driver.FindElement(By.Name("Address")).Clear();
            driver.FindElement(By.Name("Address")).SendKeys("Quận 12");
            Thread.Sleep(1000);
            driver.FindElement(By.Name("Description")).Clear();
            driver.FindElement(By.Name("Description")).SendKeys("Phòng đẹp rộng rãi, thoáng mát");
            Thread.Sleep(1000);
            driver.FindElement(By.Name("Price")).Clear();
            driver.FindElement(By.Name("Price")).SendKeys("100");
            Thread.Sleep(1000);
            var button1 = driver.FindElement(By.CssSelector("button.btn.btn-primary"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", button1);
            Thread.Sleep(2000);
            // Nhấn nút
            button1.Click();
            Thread.Sleep(2000);
            driver.Navigate().GoToUrl("http://localhost:5297/Room/SeeRoom");
            Thread.Sleep(1000);
            //


            // dang nhap customer để đặt phòng
            driver.Navigate().GoToUrl("http://localhost:5297/Account/Login");

            Thread.Sleep(1000);

            driver.FindElement(By.Name("email")).Clear();
            driver.FindElement(By.Name("email")).SendKeys("dangkhoa18205@gmail.com");
            Thread.Sleep(1000);

            driver.FindElement(By.Id("password")).Clear();
            driver.FindElement(By.Id("password")).SendKeys("123456");
            Thread.Sleep(1000);
            var buttonLogin1 = driver.FindElement(By.CssSelector("button.submitBtn"));
            buttonLogin1.Click();

            Thread.Sleep(2000);
            driver.Navigate().GoToUrl("http://localhost:5297/");

            // đặt phòng
            var seeRoomLink = driver.FindElement(By.CssSelector("a[href*='/Room/SeeRoom']"));
            seeRoomLink.Click();
            Thread.Sleep(1000);
            var roomLink = driver.FindElement(By.CssSelector("a[href='/Room/Details/1']"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", roomLink);
            Thread.Sleep(1000);
            var button2 = driver.FindElement(By.CssSelector("a.btn"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", button2);

            Thread.Sleep(2000);

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            string startTime = "2024-11-22T08:00";
            js.ExecuteScript($"document.getElementById('StartTime').value = '{startTime}';");
            Thread.Sleep(1000);


            string endTime = "2024-11-23T08:00";
            js.ExecuteScript($"document.getElementById('EndTime').value = '{endTime}';");
            Thread.Sleep(1000);

            string startTimeDisplay = "8:00 SA";
            string endTimeDisplay = "8:00 SA";

            js.ExecuteScript($"document.getElementById('StartTime').insertAdjacentHTML('afterend', '<span> ({startTimeDisplay})</span>');");
            Thread.Sleep(1000);

            js.ExecuteScript($"document.getElementById('EndTime').insertAdjacentHTML('afterend', '<span> ({endTimeDisplay})</span>');");
            Thread.Sleep(1000);

            var buttonMomo = driver.FindElement(By.XPath("//button[contains(text(), 'Momo')]"));
            buttonMomo.Click();
            Thread.Sleep(1000);

            var button3 = driver.FindElement(By.XPath("//button[contains(text(), 'Đặt phòng')]"));
            button3.Click();
            Thread.Sleep(4000);


            // manager comfirm 
            driver.Navigate().GoToUrl("http://localhost:5297/Account/Login");
            Thread.Sleep(1000);

            driver.FindElement(By.Name("email")).Clear();
            driver.FindElement(By.Name("email")).SendKeys("khoamg@gmail.com");
            Thread.Sleep(1000);

            driver.FindElement(By.Id("password")).Clear();
            driver.FindElement(By.Id("password")).SendKeys("123456");
            Thread.Sleep(1000);
            var buttonLogin2 = driver.FindElement(By.CssSelector("button.submitBtn"));
            buttonLogin2.Click();

            Thread.Sleep(2000);
            driver.Navigate().GoToUrl("http://localhost:5297/");

            driver.Navigate().GoToUrl("http://localhost:5297/Booking/ManageBookings");
            Thread.Sleep(1000);
            string expectedStartTime = "11/22/2024 8:00 AM";
            string expectedEndTime = "11/23/2024 8:00 AM";
            bool bookingFound = false;
            var rows = driver.FindElements(By.CssSelector("table.table tbody tr"));

            foreach (var row in rows)
            {
                var columns = row.FindElements(By.TagName("td"));

                if (columns.Count == 5)
                {
                    string startTimes = columns[1].Text;
                    string endTimes = columns[2].Text;

                    if (startTimes == expectedStartTime && endTimes == expectedEndTime)
                    {
                        bookingFound = true;


                        var form = columns[4].FindElement(By.TagName("form"));


                        var selectElement = new SelectElement(form.FindElement(By.Name("newStatus")));
                        selectElement.SelectByValue("Confirmed");
                        Thread.Sleep(1000);

                        var submitButton = form.FindElement(By.TagName("button"));
                        submitButton.Click();


                        Thread.Sleep(2000);

                        break;
                    }
                }
            }
            Thread.Sleep(5000);


            //xem thong bao va lich
            driver.Navigate().GoToUrl("http://localhost:5297/Account/Login");
            Thread.Sleep(1000);

            driver.FindElement(By.Name("email")).Clear();
            driver.FindElement(By.Name("email")).SendKeys("dangkhoa18205@gmail.com");
            Thread.Sleep(1000);

            driver.FindElement(By.Id("password")).Clear();
            driver.FindElement(By.Id("password")).SendKeys("123456");
            Thread.Sleep(1000);
            var buttonLogin4 = driver.FindElement(By.CssSelector("button.submitBtn"));
            buttonLogin4.Click();

            Thread.Sleep(2000);
            driver.Navigate().GoToUrl("http://localhost:5297/");

            var bellIcon = driver.FindElement(By.CssSelector(".notification-icon"));

            bellIcon.Click();
            Thread.Sleep(3000);

            var calendarLink = driver.FindElement(By.XPath("//a[contains(text(), '📆Lịch')]"));

            calendarLink.Click();
            Thread.Sleep(2000);

            //admin
            driver.Navigate().GoToUrl("http://localhost:5297/Account/Login");
            Thread.Sleep(1000);

            driver.FindElement(By.Name("email")).Clear();
            driver.FindElement(By.Name("email")).SendKeys("khoaadm@gmail.com");
            Thread.Sleep(1000);

            driver.FindElement(By.Id("password")).Clear();
            driver.FindElement(By.Id("password")).SendKeys("123456");
            Thread.Sleep(1000);
            var buttonLogin5 = driver.FindElement(By.CssSelector("button.submitBtn"));
            buttonLogin5.Click();

            Thread.Sleep(2000);
            driver.Navigate().GoToUrl("http://localhost:5297/");
            Thread.Sleep(5000);

            // tạo và cấp quyền
            driver.Navigate().GoToUrl("http://localhost:5297/Admin/profileUsers");


            driver.FindElement(By.Id("username")).SendKeys("testuser");
            Thread.Sleep(2000);
            driver.FindElement(By.Id("email")).SendKeys("testuser@gmail.com");
            Thread.Sleep(2000);
            driver.FindElement(By.Id("password")).SendKeys("123456");
            Thread.Sleep(2000);
            // Chọn quyền từ dropdown
            new SelectElement(driver.FindElement(By.Id("role"))).SelectByValue("manager");

            // Nhấn nút "Tạo Tài Khoản"
            Thread.Sleep(2000);
            driver.FindElement(By.CssSelector("button[type='submit']")).Click();
            Thread.Sleep(2000);

            // xem doanh thu
            driver.Navigate().GoToUrl("http://localhost:5297/Admin/Revenue");
            Thread.Sleep(5000);

            // xem xoa sua phong
            driver.Navigate().GoToUrl("http://localhost:5297/Admin/Rooms");
            Thread.Sleep(5000);
        }

        [TearDown]
        public void CloseTest()
        {
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose();
            }
        }
    }
}
