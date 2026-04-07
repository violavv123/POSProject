using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace POSProject
{
    public class ShiftService
    {
        public CashierShiftModel? GetOpenShiftByUser(int userId)
        {
            using (var conn = Db.GetConnection())
            {
                conn.Open();
                string query = @"SELECT ""Id"", ""PerdoruesiId"", ""Opened_At"", ""Closed_At"", ""OpeningBalance"", ""ClosingBalanceExpected""
                                 ,""ClosingBalanceActual"", ""Difference"", ""Status"", ""Koment""
                                 FROM ""CashierShifts""
                                 WHERE ""PerdoruesiId"" = @userId AND ""Status"" = 'OPEN'
                                 LIMIT 1;";
                using (var cmd = new Npgsql.NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (!reader.Read())
                            return null;
                        return new CashierShiftModel
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            PerdoruesiId = reader.GetInt32(reader.GetOrdinal("PerdoruesiId")),
                            Opened_At = reader.GetDateTime(reader.GetOrdinal("Opened_At")),
                            Closed_At = reader.IsDBNull(reader.GetOrdinal("Closed_At")) ? null : reader.GetDateTime(reader.GetOrdinal("Closed_At")),
                            OpeningBalance = reader.GetDecimal(reader.GetOrdinal("OpeningBalance")),
                            ClosingBalanceExpected = reader.IsDBNull(reader.GetOrdinal("ClosingBalanceExpected")) ? null : reader.GetDecimal(reader.GetOrdinal("ClosingBalanceExpected")),
                            ClosingBalanceActual = reader.IsDBNull(reader.GetOrdinal("ClosingBalanceActual")) ? null : reader.GetDecimal(reader.GetOrdinal("ClosingBalanceActual")),
                            Difference = reader.IsDBNull(reader.GetOrdinal("Difference")) ? null : reader.GetDecimal(reader.GetOrdinal("Difference")),
                            Status = reader.GetString(reader.GetOrdinal("Status")),
                            Koment = reader.IsDBNull(reader.GetOrdinal("Koment")) ? null : reader.GetString(reader.GetOrdinal("Koment"))
                        };

                    }
                }
            }
        }

        public CashierShiftModel? GetOpenShift()
        {
            using (var conn = Db.GetConnection())
            {
                conn.Open();
                string query = @"SELECT ""Id"", ""PerdoruesiId"", ""Opened_At"", ""Closed_At"",
                                ""OpeningBalance"", ""ClosingBalanceExpected"",
                                ""ClosingBalanceActual"", ""Difference"", ""Status"", ""Koment""
                                 FROM ""CashierShifts""
                                 WHERE TRIM(UPPER(""Status"")) = 'OPEN'
                                 ORDER BY ""Opened_At"" DESC, ""Id"" DESC
                                 LIMIT 1;";

                using (var cmd = new NpgsqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                        return null;

                    return new CashierShiftModel
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        PerdoruesiId = reader.GetInt32(reader.GetOrdinal("PerdoruesiId")),
                        Opened_At = reader.GetDateTime(reader.GetOrdinal("Opened_At")),
                        Closed_At = reader.IsDBNull(reader.GetOrdinal("Closed_At")) ? null : reader.GetDateTime(reader.GetOrdinal("Closed_At")),
                        OpeningBalance = reader.GetDecimal(reader.GetOrdinal("OpeningBalance")),
                        ClosingBalanceExpected = reader.IsDBNull(reader.GetOrdinal("ClosingBalanceExpected")) ? null : reader.GetDecimal(reader.GetOrdinal("ClosingBalanceExpected")),
                        ClosingBalanceActual = reader.IsDBNull(reader.GetOrdinal("ClosingBalanceActual")) ? null : reader.GetDecimal(reader.GetOrdinal("ClosingBalanceActual")),
                        Difference = reader.IsDBNull(reader.GetOrdinal("Difference")) ? null : reader.GetDecimal(reader.GetOrdinal("Difference")),
                        Status = reader.GetString(reader.GetOrdinal("Status")),
                        Koment = reader.IsDBNull(reader.GetOrdinal("Koment")) ? null : reader.GetString(reader.GetOrdinal("Koment"))
                    };
                }
            }
        }

        public CashierShiftModel? GetLastClosedShift()
        {
            using (var conn = Db.GetConnection())
            {
                conn.Open();
                string query = @"SELECT ""Id"", ""PerdoruesiId"", ""Opened_At"", ""Closed_At"",
                                ""OpeningBalance"", ""ClosingBalanceExpected"",
                                ""ClosingBalanceActual"", ""Difference"", ""Status"", ""Koment""
                                 FROM ""CashierShifts""
                                 WHERE TRIM(UPPER(""Status"")) = 'CLOSED'
                                   AND ""Closed_At"" IS NOT NULL
                                 ORDER BY ""Closed_At"" DESC, ""Id"" DESC
                                 LIMIT 1;";

                using (var cmd = new NpgsqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                        return null;

                    return new CashierShiftModel
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        PerdoruesiId = reader.GetInt32(reader.GetOrdinal("PerdoruesiId")),
                        Opened_At = reader.GetDateTime(reader.GetOrdinal("Opened_At")),
                        Closed_At = reader.IsDBNull(reader.GetOrdinal("Closed_At")) ? null : reader.GetDateTime(reader.GetOrdinal("Closed_At")),
                        OpeningBalance = reader.GetDecimal(reader.GetOrdinal("OpeningBalance")),
                        ClosingBalanceExpected = reader.IsDBNull(reader.GetOrdinal("ClosingBalanceExpected")) ? null : reader.GetDecimal(reader.GetOrdinal("ClosingBalanceExpected")),
                        ClosingBalanceActual = reader.IsDBNull(reader.GetOrdinal("ClosingBalanceActual")) ? null : reader.GetDecimal(reader.GetOrdinal("ClosingBalanceActual")),
                        Difference = reader.IsDBNull(reader.GetOrdinal("Difference")) ? null : reader.GetDecimal(reader.GetOrdinal("Difference")),
                        Status = reader.GetString(reader.GetOrdinal("Status")),
                        Koment = reader.IsDBNull(reader.GetOrdinal("Koment")) ? null : reader.GetString(reader.GetOrdinal("Koment"))
                    };
                }
            }
        }
        public void AddCashMovement(int shiftId, string tipi, decimal shuma, string? arsyeja, string? koment, int perdoruesiId)
        {
            using (var conn = Db.GetConnection())
            {
                conn.Open();

                string query = @"
                                INSERT INTO ""CashMovements""
                                (""ShiftId"", ""Tipi"", ""Shuma"", ""Arsyeja"", ""Koment"", ""PerdoruesiId"", ""Created_At"")
                                VALUES
                                (@shiftId, @tipi, @shuma, @arsyeja, @koment, @perdoruesiId, CURRENT_TIMESTAMP);";

                using (var cmd = new Npgsql.NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@shiftId", shiftId);
                    cmd.Parameters.AddWithValue("@tipi", tipi);
                    cmd.Parameters.AddWithValue("@shuma", shuma);
                    cmd.Parameters.AddWithValue("@arsyeja", (object?)arsyeja ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@koment", (object?)koment ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@perdoruesiId", perdoruesiId);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        public int OpenShift(int userId, decimal openingBalance, string? koment)
        {
            using (var conn = Db.GetConnection())
            {
                conn.Open();

                using (var tx = conn.BeginTransaction())
                {
                    try
                    {
                        string checkOpenQuery = @"
                    SELECT ""Id"", ""PerdoruesiId""
                    FROM ""CashierShifts""
                    WHERE TRIM(UPPER(""Status"")) = 'OPEN'
                    LIMIT 1;";

                        using (var checkCmd = new NpgsqlCommand(checkOpenQuery, conn, tx))
                        using (var reader = checkCmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int openShiftId = reader.GetInt32(reader.GetOrdinal("Id"));
                                int openUserId = reader.GetInt32(reader.GetOrdinal("PerdoruesiId"));
                                throw new Exception($"Ekziston tashmë një ndërrim i hapur. ShiftId: {openShiftId}, UserId: {openUserId}.");
                            }
                        }

                        string query = @"
                    INSERT INTO ""CashierShifts""
                    (""PerdoruesiId"", ""Opened_At"", ""OpeningBalance"", ""Status"", ""Koment"")
                    VALUES (@userId, CURRENT_TIMESTAMP, @openingBalance, 'OPEN', @comment)
                    RETURNING ""Id"";";

                        using (var cmd = new NpgsqlCommand(query, conn, tx))
                        {
                            cmd.Parameters.AddWithValue("@userId", userId);
                            cmd.Parameters.AddWithValue("@openingBalance", openingBalance);
                            cmd.Parameters.AddWithValue("@comment", (object?)koment ?? DBNull.Value);

                            int newId = Convert.ToInt32(cmd.ExecuteScalar());
                            tx.Commit();
                            return newId;
                        }
                    }
                    catch (PostgresException ex) when (ex.SqlState == "23505")
                    {
                        tx.Rollback();
                        throw new Exception("Ekziston tashmë një ndërrim OPEN. Nuk mund të hapet një tjetër derisa ai të mbyllet.");
                    }
                    catch
                    {
                        tx.Rollback();
                        throw;
                    }
                }
            }
        }

        public void CloseShift(int shiftId, decimal expected, decimal actual, decimal difference, string? koment)
        {
            using (var conn = Db.GetConnection())
            {
                conn.Open();
                string query = @"UPDATE ""CashierShifts""
                               SET ""Closed_At"" = CURRENT_TIMESTAMP,
                                   ""ClosingBalanceExpected"" = @expected,
                                   ""ClosingBalanceActual"" = @actual,
                                   ""Difference"" = @difference,
                                   ""Koment"" = COALESCE(@comment, ""Koment""),
                                   ""Status"" = 'CLOSED'
                                   WHERE ""Id"" = @shiftId AND ""Status"" = 'OPEN';";
                using (var cmd = new Npgsql.NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@shiftId", shiftId);
                    cmd.Parameters.AddWithValue("@expected", expected);
                    cmd.Parameters.AddWithValue("@actual", actual);
                    cmd.Parameters.AddWithValue("@difference", difference);
                    cmd.Parameters.AddWithValue("@comment", (object?)koment ?? DBNull.Value);
                    int rows = cmd.ExecuteNonQuery();
                    if (rows == 0)
                        throw new Exception("Ndërrimi nuk u gjet ose është mbyllur tashmë.");
                }
            }
        }

        public decimal GetCashSales(int shiftId)
        {
            using (var conn = Db.GetConnection())
            {
                conn.Open();
                string query = @"SELECT COALESCE(SUM(ep.""ShumaNeValuteBaze""),0)
                                 FROM ""EkzekutimiPageses"" ep
                                 INNER JOIN ""MenyratPageses"" mp
                                 ON ep.""MenyraPagesesId"" = mp.""Id""
                                 WHERE ep.""ShiftId"" = @shiftId AND mp.""Tipi"" = 'CASH'
                                 AND COALESCE(ep.""Statusi"", 'Kompletuar') = 'Kompletuar';";
                using (var cmd = new Npgsql.NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@shiftId", shiftId);
                    return Convert.ToDecimal(cmd.ExecuteScalar());
                }
            }
        }

        public decimal GetCashIn(int shiftId)
        {
            using (var conn = Db.GetConnection())
            {
                conn.Open();
                string query = @"SELECT COALESCE(SUM(""Shuma""),0) 
                                 FROM ""CashMovements""
                                 WHERE ""ShiftId"" = @shiftId AND ""Tipi"" = 'IN';";
                using (var cmd = new Npgsql.NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@shiftId", shiftId);
                    return Convert.ToDecimal(cmd.ExecuteScalar());
                }
            }
        }

        public decimal GetCashOut(int shiftId)
        {
            using (var conn = Db.GetConnection())
            {
                conn.Open();
                string query = @"SELECT COALESCE(SUM(""Shuma""),0)
                                 FROM ""CashMovements""
                                 WHERE ""ShiftId"" = @shiftId AND ""Tipi"" = 'OUT';";
                using (var cmd = new Npgsql.NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@shiftId", shiftId);
                    return Convert.ToDecimal(cmd.ExecuteScalar());
                }
            }
        }

        public decimal CalculateExpectedCash(decimal openingBalance, decimal cashSales, decimal cashIn, decimal cashOut)
        {
            return openingBalance + cashSales + cashIn - cashOut;
        }

        public decimal GetSuggestedOpeningBalance()
        {
            var lastClosedShift = GetLastClosedShift();
            if (lastClosedShift == null)
                return 0m;

            if (lastClosedShift.ClosingBalanceActual.HasValue && lastClosedShift.ClosingBalanceActual.Value >= 0)
                return lastClosedShift.ClosingBalanceActual.Value;

            if (lastClosedShift.ClosingBalanceExpected.HasValue && lastClosedShift.ClosingBalanceExpected.Value >= 0)
                return lastClosedShift.ClosingBalanceExpected.Value;

            return 0m;
        }

        public decimal GetAvailableCash(int shiftId)
        {
            var openShift = GetOpenShift();
            if (openShift == null || openShift.Id != shiftId)
                throw new Exception("Shift-i aktiv nuk u gjet.");

            decimal cashSales = GetCashSales(shiftId);
            decimal cashIn = GetCashIn(shiftId);
            decimal cashOut = GetCashOut(shiftId);

            return CalculateExpectedCash(openShift.OpeningBalance, cashSales, cashIn, cashOut);
        }
    }
}
