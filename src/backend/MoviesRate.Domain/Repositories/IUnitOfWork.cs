﻿namespace MoviesRate.Domain.Repositories;

public interface IUnitOfWork
{
    Task Commit();
}