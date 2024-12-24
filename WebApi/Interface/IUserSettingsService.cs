namespace WebApi.Interface;

public interface IUserSettingsService<T>
{
    public Task<T> ChangeUserName();
    public Task<T> ChangePassword();
    public Task<T> ChangePhone();
}