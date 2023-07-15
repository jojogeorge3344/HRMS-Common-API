namespace Chef.Common.Services;

public interface IServiceFactory
{
    /// <summary>
    ///This is a Unit of work session block. It applies transaction to all the database commands included within this block. 
    ///This block supersedes any other session block declare inside that and combines everything into one block.
    /// </summary>
    /// <param name="isolationLevel">Transaction Isolation Level</param>
    /// <returns>IUnitOfWorkSession</returns>
    //IUnitOfWorkSession UnitOfWorkSession(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted); 
}
