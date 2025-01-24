﻿namespace MoviesRate.Domain.Repositories.User;

public interface IWriteUserRepository
{
    Task Add(Entities.User user);
    void Update(Entities.User user);
    void Delete(Entities.User user);    
}