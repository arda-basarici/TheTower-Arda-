namespace Game
{
    public static class SaveMigrator
    {
        public static WalletData MigrateWalletData(WalletData oldData)
        {
            if (oldData == null) return new WalletData();
            if (oldData.tokens == 0) oldData.tokens = 0; 
            return oldData;
        }
    }
}