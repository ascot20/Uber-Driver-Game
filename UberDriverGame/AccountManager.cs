using System;
using System.Collections.Generic;
using Utilities;

class AccountManager
{

    private const int firstRowPos = 0;
    private const int thirdRowPos = 2;
    private const int fourthRowPos = 3;
    private const int minEarnings = 10;
    private const int maxEarnings = 100;
    private const int minCostOfRepairs = 200;
    private const int maxCostOfRepairs = 500;

    private decimal previousBalance;
    private List<BufferString> accountDashboardBuffers;
    private List<BufferString> transactionBuffers;

    static Random generator = new Random();

    public AccountManager(Driver driver, ScreenBuffer screenBuffer)
    {
        this.previousBalance = driver.totalEarnings;
        this.accountDashboardBuffers = new List<BufferString>();
        this.transactionBuffers = new List<BufferString>();
        updateAccountDashboard(driver, screenBuffer);
    }

    public void updateAccountDashboard(Driver driver, ScreenBuffer screenBuffer)
    {
        decimal driverEarnings = driver.totalEarnings;
        string driverDashboard;

        if (driverEarnings < 0)
        {
            driverDashboard = "Total Earnings: - £" + Math.Abs(driverEarnings) + ".00";
        }

        else
        {
            driverDashboard = "Total Earnings: £" + driver.totalEarnings + ".00";
        }

        Text.clearBufferStrings(this.accountDashboardBuffers, screenBuffer);

        BufferString s = Text.createRightAlignedBufferString(driverDashboard, firstRowPos);
        screenBuffer.writeLine(s);

        this.accountDashboardBuffers.Add(s);
    }

    public void addEarnings(Driver driver, ScreenBuffer screenBuffer)
    {
        int earning = generator.Next(minEarnings, maxEarnings + 1);

        driver.totalEarnings += earning;
        updateAccountDashboard(driver, screenBuffer);
        showTransaction(driver, screenBuffer);
        this.previousBalance = driver.totalEarnings;
    }

    public void deductAccount(Driver driver, ScreenBuffer screenBuffer)
    {
        int deduction = generator.Next(minCostOfRepairs, maxCostOfRepairs + 1);

        driver.totalEarnings -= deduction;
        this.updateAccountDashboard(driver, screenBuffer);
        this.showTransaction(driver, screenBuffer);
        this.previousBalance = driver.totalEarnings;
    }

    private void showTransaction(Driver driver, ScreenBuffer screenBuffer)
    {
        Text.clearBufferStrings(this.transactionBuffers, screenBuffer);
        if (previousBalance > driver.totalEarnings)
        {
            decimal deduction = this.previousBalance - driver.totalEarnings;
            string transactionNotification;
            string borrowNotification;

            if (driver.totalEarnings < 0)
            {
                borrowNotification = "- £" + Math.Abs(driver.totalEarnings) + ".00" + " borrowed from the bank for repairs.";
                BufferString borrowNotificationBuffer = Text.createRightAlignedBufferString(borrowNotification, fourthRowPos);
                screenBuffer.writeLine(borrowNotificationBuffer);
                this.transactionBuffers.Add(borrowNotificationBuffer);
            }
            transactionNotification = "- £" + deduction + ".00 " + " was used for repairs";
            BufferString transactionNotificationBuffer = Text.createRightAlignedBufferString(transactionNotification, thirdRowPos);

            screenBuffer.writeLine(transactionNotificationBuffer);

            this.transactionBuffers.Add(transactionNotificationBuffer);

        }
        else
        {
            decimal earning = driver.totalEarnings - previousBalance;
            BufferString transactionNotivationBuffer;
            string transactionNotification = "+ £" + earning + ".00" + " added to your account for last ride.";
            transactionNotivationBuffer = Text.createRightAlignedBufferString(transactionNotification, thirdRowPos);

            screenBuffer.writeLine(transactionNotivationBuffer);
            this.transactionBuffers.Add(transactionNotivationBuffer);
        }
    }
}
