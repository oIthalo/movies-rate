﻿namespace MoviesRate.Domain.Repositories.User;

public interface IWriteUserRepository
{
    Task Add(Entities.User user);
}