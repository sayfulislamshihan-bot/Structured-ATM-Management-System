//Account Data
using System.ComponentModel.Design;

string[,] accountInfo = new string[,]
{
    {"Rahim", "1001", "1234", "50000", "false", "0" }, //name, accountNo, pin, balance, status, maliciusActivity
    {"Karim", "1002", "5678", "75000", "false", "0" },
    {"Sayful", "1003", "4321", "25000", "false", "0" },
};


int MaliciousActivity = 0;

welcomeMessage();
while (true)
{
    bool isLogout = true;
    //Taking input from users to perform operation.
    Console.WriteLine("");
    Console.WriteLine("Please Enter a Choice");
    Console.WriteLine("1. Login");
    Console.WriteLine("2. Exit");
    Console.WriteLine("");
    Console.Write("Enter your chioce: ");
    bool isValid = int.TryParse(Console.ReadLine(), out int choice);
    switch (choice)
    {
        case 1:
            atmWelcome();
            Console.Write("Enter Account No: ");
            bool isBool = int.TryParse(Console.ReadLine(), out int accNo);
            if (isBool && enterAccountNo(accountInfo, accNo))
            {
                while (isLogout)
                {
                    bool isValidAcc = enterAccountNo(accountInfo, accNo);
                    if (accountInfo[indexOfValidAcc(accNo), 4] == "true")
                    {
                        Console.WriteLine("Account is blocked!");
                        isLogout = false;
                    }
                    else
                    {
                        if (isValidAcc && isBool)
                        {
                            Console.Write("Enter the 4 digit pin No: ");
                            bool isBoolPin = int.TryParse(Console.ReadLine(), out int pinNo);
                            bool validPin = CheckPin(accountInfo, pinNo);
                            if (validPin && isBoolPin)
                            {
                                welcomeMessageIntoATM(accNo);
                                while (isLogout)
                                {
                                    optionsInAccountToChoose();
                                    bool isValidChoice = int.TryParse(Console.ReadLine(), out int Choice);
                                    if (isValidChoice)
                                    {
                                        switch (Choice)
                                        {
                                            case 1:
                                                showBalance(accountInfo, accNo);
                                                break;

                                            case 2:
                                                dipositMoney(accNo);
                                                break;

                                            case 3:
                                                withdrawMoney(accNo);
                                                break;

                                            case 4:
                                                transferMoney(accNo);
                                                break;

                                            case 5:
                                                accountInfomation(accNo);
                                                break;
                                            case 6:
                                                changePinNo(accNo);
                                                break;
                                            case 7:
                                                Console.WriteLine("Logout Successful see you again " + UserName(accNo) + " :)");
                                                isLogout = false;

                                                break;
                                            default:
                                                Console.WriteLine("Invalid Choice!");
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid input!");
                                    }

                                }
                            }
                            else
                            {
                                Console.WriteLine("Incorrect Pin no!");
                                Console.WriteLine("");
                                maliciousActivity(accNo);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Account not found");
                            break;
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid Account Input please enter correct account no!");
            }
            break;

        case 2:
            endMessage();
            Console.ReadKey();
            return;

        default:
            Console.WriteLine("Invalid Choice!");
            break;
    }
}

string UserName(int accNo)
{
    return accountInfo[indexOfValidAcc(accNo), 0];
}

void endMessage()
{
    Console.WriteLine("");
    Console.WriteLine("Thanks for Banking with us. Have a good day :)");
    Console.WriteLine("----------------------------------------------");
    Console.Write("         Press any key to exit");
}

void maliciousActivity(int accNo)
{
    if (MaliciousActivity == 2)
    {
        accountInfo[indexOfValidAcc(accNo), 4] = "true";
    }
    else
    {
        MaliciousActivity++;
    }
}
void changePinNo(int accNo)
{
    Console.Write("Enter old pin: ");
    bool isInputValid = int.TryParse(Console.ReadLine(), out int oldPin);
    if (isInputValid)
    {
        if (CheckPin(accountInfo, oldPin))
        {
            Console.Write("Enter a new 4 digit pin no: ");
            bool isValidPin = int.TryParse(Console.ReadLine(), out int newPin);
            if (isValidPin)
            {
                int AccPin = conIntoInt(accountInfo[indexOfValidAcc(accNo), 2]);
                AccPin = newPin;
                string NewPinSet = AccPin.ToString();
                accountInfo[indexOfValidAcc(accNo), 2] = NewPinSet;
                Console.WriteLine("");
                Console.WriteLine("New Account pin set to: " + accountInfo[indexOfValidAcc(accNo), 2]);
            }
            else
            {
                Console.WriteLine("Wrong pin type enter only integer value");
            }
        }

    }
    else
    {
        Console.WriteLine("Invalid pin");
    }
}

void accountInfomation(int accNo)
{
    Console.WriteLine("");
    Console.WriteLine("Name: " + accountInfo[indexOfValidAcc(accNo), 0]);
    Console.WriteLine("AccountNo: " + accountInfo[indexOfValidAcc(accNo), 1]);
    Console.WriteLine("Pin: " + accountInfo[indexOfValidAcc(accNo), 2]);
    Console.WriteLine("Balance: " + accountInfo[indexOfValidAcc(accNo), 3]);
}
void withdrawMoney(int accNo)
{
    Console.Write("Enter the amount you want to withdraw: ");
    bool isInputValid = int.TryParse(Console.ReadLine(), out int amount);
    if (isInputValid && (amount > 0))
    {
        int AccBalance = conIntoInt(accountInfo[indexOfValidAcc(accNo), 3]);
        AccBalance = AccBalance - amount;
        if (AccBalance >= 500)
        {
            string newAccBalance = AccBalance.ToString();
            accountInfo[indexOfValidAcc(accNo), 3] = newAccBalance;
            Console.WriteLine("");
            Console.WriteLine("New Balance: " + accountInfo[indexOfValidAcc(accNo), 3]);
        }
        else
        {
            Console.WriteLine("Insufficient Balance!");
        }
    }
    else
    {
        Console.WriteLine("Invalid amount");
    }
}

void dipositMoney(int accNo)
{
    Console.Write("Enter the amount you want to diposit: ");
    bool isInputValid = int.TryParse(Console.ReadLine(), out int amount);
    if (isInputValid && (amount > 0))
    {
        int AccBalance = conIntoInt(accountInfo[indexOfValidAcc(accNo), 3]);
        AccBalance = AccBalance + amount;
        accountInfo[indexOfValidAcc(accNo), 3] = AccBalance.ToString();
        Console.WriteLine("");
        Console.WriteLine("New Balance: " + accountInfo[indexOfValidAcc(accNo), 3]);
    }
    else
    {
        Console.WriteLine("Invalid amount");
    }
}
void transferMoney(int accNo)
{
    Console.Write("Enter the account no you want to send money: ");
    bool isInputValid = int.TryParse(Console.ReadLine(), out int accountToTransferNo);
    Console.Write("Enter the amount: ");
    bool isInputValidAmount = int.TryParse(Console.ReadLine(), out int amountToTransfer);

    if (checkTransferAccountNo(accountToTransferNo) && (amountToTransfer > 0))
    {
        int fromAccBalance = conIntoInt(accountInfo[indexOfValidAcc(accNo), 3]);//Account balance string is converted into number
        int toAccBalance = conIntoInt(accountInfo[indexOfValidAcc(accountToTransferNo), 3]);

        fromAccBalance = fromAccBalance - amountToTransfer;
        toAccBalance = toAccBalance + amountToTransfer;

        string fromNewAccBalance = fromAccBalance.ToString();
        string toNewAccBalance = toAccBalance.ToString();

        accountInfo[indexOfValidAcc(accNo), 3] = fromNewAccBalance;
        accountInfo[indexOfValidAcc(accountToTransferNo), 3] = toNewAccBalance;
        Console.WriteLine("");
        Console.WriteLine("New Account Balance: " + accountInfo[indexOfValidAcc(accNo), 3]);
    }
    else
    {
        Console.WriteLine("Please enter right acount no as well as right amount you want to transfer");
    }
}
string conIntoString(int number)
{
    string newBalance = number.ToString();
    return newBalance;
}

void showBalance(string[,] accountInfo, int accNo)
{
    bool index = int.TryParse(accountInfo[indexOfValidAcc(accNo), 3], out int rowIndex);
    Console.WriteLine("Balance: " + rowIndex);

}
void welcomeMessageIntoATM(int accNo)
{
    int index = indexOfValidAcc(accNo);
    Console.WriteLine("============================");
    Console.WriteLine("       WELCOME TO ATM");
    Console.WriteLine("============================");
    Console.WriteLine("Login Successful Welcome, " + accountInfo[indexOfValidAcc(accNo), 0]);
    Console.WriteLine("");
}
bool CheckPin(string[,] accountInfo, int pinNo)
{

    for (int i = 0; i < accountInfo.GetLength(0); i++)
    {
        if (conIntoInt(accountInfo[i, 2]) == pinNo)
            return true;

    }
    return false;

}
bool enterAccountNo(string[,] accountInfo, int accountNo)
{

    for (int i = 0; i < accountInfo.GetLength(0); i++)
    {
        int StringToInt = conIntoInt(accountInfo[i, 1]);
        if (StringToInt == accountNo)
        {
            return true;
        }
    }
    return false;
}

bool checkTransferAccountNo(int accountNo)
{
    int ValidAcc = 0;
    for (int i = 0; i < accountInfo.GetLength(0); i++)
    {
        int StringToInt = conIntoInt(accountInfo[i, 1]);
        if (StringToInt == accountNo)
        {
            ValidAcc++;
        }
    }

    if (ValidAcc == 1)
    {
        return true;
    }
    else
    {
        return false;
    }
}

int indexOfValidAcc(int accountNo)
{
    int res = -1;
    for (int i = 0; i < accountInfo.GetLength(0); i++)
    {
        int StringToInt = conIntoInt(accountInfo[i, 1]);
        if (StringToInt == accountNo)
        {
            res = i;
        }
    }
    return res;
}

int conIntoInt(string str)
{
    bool isValid = int.TryParse(str, out int number);
    return number;

}

void optionsInAccountToChoose()
{
    Console.WriteLine("");
    Console.WriteLine("1.Check Balance");
    Console.WriteLine("2.Deposit Money");
    Console.WriteLine("3.Withdraw Money");
    Console.WriteLine("4.Send Money");
    Console.WriteLine("5.Account Information");
    Console.WriteLine("6.Change PIN");
    Console.WriteLine("7.Logout");
    Console.WriteLine("");
    Console.Write("Enter your choice: ");
}

void atmWelcome()
{
    Console.WriteLine("============================================");
    Console.WriteLine("               ATM Ststem");
    Console.WriteLine("============================================");
}



void welcomeMessage()
{
    //Welcome Message
    Console.WriteLine("=====================================================================");
    Console.WriteLine("              Welcome to the ATM Management Ststem");
    Console.WriteLine("=====================================================================");
    Console.WriteLine("");
}

