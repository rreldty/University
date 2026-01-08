using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using University.Dto.Base;

namespace University.Dao.Base
{
    public class JobSqlDao : BaseDao<JobSqlDto>
    {
        #region "Abstract Class Implementation"

        public JobSqlDao()
        {
            this.MainDataSource = DataSource.University;
        }

        protected override Mapper<JobSqlDto> GetMapper()
        {
            Mapper<JobSqlDto> mapDto = new JobSqlMappingDto();
            return mapDto;
        }

        #endregion

        #region Select Data
        public List<JobSqlDto> GetList(string strFilter)
        {
            string strSql = "SELECT "
                        + "sj.Name[JobName] "
                        + ",sj.description [JobDescription] "
                        + ", CONVERT(VARCHAR, sja.start_execution_date, 105) +' ' + CONVERT(VARCHAR(5), sja.start_execution_date, 108)[StartRunningDate] "
                        + ", CONVERT(VARCHAR, sja.last_executed_step_date, 105) +' ' + CONVERT(VARCHAR(5), sja.last_executed_step_date, 108)[LastRunningDate] "
                        + ", CONVERT(VARCHAR, sja.next_scheduled_run_date, 105) + ' ' + CONVERT(VARCHAR(5), sja.next_scheduled_run_date, 108)[NextRunningDate] "
                        + ",CASE "
                            + "WHEN sja.start_execution_date IS NULL THEN 'Not running' "
                            + "WHEN sja.start_execution_date IS NOT NULL AND sja.stop_execution_date IS NULL THEN 'Running' "
                            + "WHEN sja.start_execution_date IS NOT NULL AND sja.stop_execution_date IS NOT NULL THEN 'Not running' "
                        + "END AS 'RunStatus' "
                        + "FROM msdb.dbo.sysjobs sj "
                        + "JOIN msdb.dbo.sysjobactivity sja "
                        + "ON sj.job_id = sja.job_id "
                        + "WHERE 1 = 1 "
                        + "AND session_id = (SELECT MAX(session_id) FROM msdb.dbo.sysjobactivity) "
                        + " AND sj.[name] IN ('" + strFilter + "') "
                        + "";

            List<JobSqlDto> dto = this.ExecuteQuery(strSql);
            return dto;
        }
        #endregion
    }
}
