﻿using BankingManagementSystem.Model;

namespace BankingManagementSystem.Models.Setup.Account;
public class AccountListResponseModel
{
    public MessageResponseModel Response { get; set; }

    public PageSettingModel PageSetting { get; set; }
    public List<AccountModel> Data { get; set; }
}