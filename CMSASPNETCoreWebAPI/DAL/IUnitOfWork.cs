namespace CMSASPNETCoreWebAPI.DAL;

public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }
    IMemberRepository MemberRepository { get; }
    IPromotionRepository PromotionRepository { get; }
    void Save();
}