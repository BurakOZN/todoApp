namespace MssqlRepository.ReturnModel
{
    public enum State
    {
        Success = 0,
        ProcessError = 1,
        SaveError = 2,
        ProcessUnsuccess = 3,
        NotFound = 4,
        DuplicateError = 5,
        ConnectionError = 6,
    }
}
