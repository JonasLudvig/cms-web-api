using CMSASPNETCoreWebAPI.SL;

namespace CMSASPNETCoreWebAPI.DAL;

public class UnitOfWork : IUnitOfWork
{
    private readonly ModelDbContext _modelDbContext;
    private readonly UserService _userService;
    private readonly MemberService _memberService;

    private IUserRepository? _userRepository;
    public IUserRepository UserRepository => _userRepository ??= new UserRepository(_modelDbContext);

    private IMemberRepository? _memberRepository;

    public IMemberRepository MemberRepository => _memberRepository ??= new MemberRepository(_modelDbContext);

    private IPromotionRepository? _promotionRepository;

    public IPromotionRepository PromotionRepository => _promotionRepository ??= new PromotionRepository(_modelDbContext);

    public UnitOfWork(ModelDbContext modelDbContext)
    {
        _modelDbContext = modelDbContext;
    }

    public UnitOfWork(UserService userService)
    {
        _userService = userService;
    }

    public UnitOfWork(MemberService memberService)
    {
        _memberService = memberService;
    }

    public void Save()
    {
        _modelDbContext.SaveChanges();
    }
}