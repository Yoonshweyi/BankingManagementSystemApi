﻿namespace BankingManagementSystem.Models.Setup.Account;

public class AdminUserRequestModel
{
    public string? AdminUserCode { get; set; } = null!;

    public string AdminUserName { get; set; } = null!;

    public string MobileNo { get; set; } = null!;

    public string UserRoleCode { get; set; } = null!;
}