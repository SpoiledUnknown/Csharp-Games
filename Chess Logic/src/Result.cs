namespace ChessLogic
{
    public class Result
    {
        public Player Winner {  get; }
        public EndReason Reason { get; }

        public Result(Player player, EndReason reason)
        {
            this.Winner = player;
            this.Reason = reason;
        }

        public static Result Win(Player player)
        {
            return new Result(player, EndReason.Checkmate);
        }

        public static Result Draw(EndReason endReason)
        {
            return new Result(Player.None, endReason);
        }
    }
}
