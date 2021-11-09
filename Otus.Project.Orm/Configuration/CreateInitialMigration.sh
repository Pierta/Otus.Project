#!/bin/bash

## Create initial migration in Postgres database (EF migration table and initial seeding)
dotnet ef migrations add InitialMigration -v -p ../../Otus.Project.Orm -s ../../Otus.Project.CrudApi