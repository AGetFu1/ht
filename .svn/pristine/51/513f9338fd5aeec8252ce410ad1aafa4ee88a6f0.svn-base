using EMS.Code;
using EMS.Domain.Entity.ExceptionManage;
using EMS.Domain.ViewModel;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Application.ExceptionManage
{
    public class ESProvider
    {
        public static ElasticClient client = new ElasticClient(Setting.ConnectionSettings);

        public static bool Index(SysExptEntityModel entity)
        {
            var client = new ElasticClient(Setting.ConnectionSettings);
            try
            {
                //添加数据 
                //在调用下面的index方法的时候，如果没有指定使用哪个index，ElasticSearch会直接使用我们在setting中的defaultIndex，如果没有，则会自动创建 
                var index = client.Index<SysExptEntityModel>(entity,s => s.Index("exception"));
                return index.IsValid;
            }
            catch (Exception ex)
            {
                LogFactory.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType).Error("插入表Sys_Expt，ID 值为："+ entity.ToJson());
            }
            return false;
        }
        public static bool Delete(string id)
        {
            var client = new ElasticClient(Setting.ConnectionSettings);
            try
            {
                var res = client.DeleteByQuery<SysExptEntityModel>(d => d.Index("exception").Query(q => q.Match(c => c.Field(dr => dr.f_id).Query(id))));
                return res.IsValid;
            }
            catch (Exception ex)
            {
                LogFactory.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType).Error("删除表Sys_Expt失败，ID 值为：" +ex.ToJson()+id);
            }
            return false;
        }
        public static bool Update(string id,string f_itemcode, string f_description ,string f_itemname)
        {
            var client = new ElasticClient(Setting.ConnectionSettings);
            try
            {
                var res = client.UpdateByQuery<SysExptEntityModel>(d => d.Index("exception").Query(q => q.Match(m => m.Field(f=>f.f_id).Query(id))).Script(s => s.Source("ctx._source.f_description=params.f_description;ctx._source.f_itemcode=params.f_itemcode;ctx._source.f_itemname=params.f_itemname").Lang("painless").Params(p => p.Add("f_itemname", f_itemname).Add("f_description",f_description).Add("f_itemcode",f_itemcode))));
                return res.IsValid;
            }
            catch (Exception ex)
            {
                Console.WriteLine(" Excepton Message : " + ex.Message);
            }
            return false;
        }
        public List<SysExptEntity> GetAllTest(string name)
        {
            ISearchResponse<SysExptEntity> searchResults = client.Search<SysExptEntity>(s =>
            s.Query(
                    q => q.QueryString(a =>
                         a.Fields(b => b.Field(c => c.F_ItemName).Field(c => c.F_Description))
                         .Query(name)
                      )
                ));
            return searchResults.Documents.ToList();
        }
        public List<SysExptEntity> GetExptDetils(Pagination pagination, string itemId = "", string keyword = "")
        {
            SearchDescriptor<SysExptEntityModel> searchDescriptor = new SearchDescriptor<SysExptEntityModel>();
            if (!string.IsNullOrEmpty(itemId))
            {
                searchDescriptor = searchDescriptor.Index("exception").Query(q => q.Bool(a => a.Must(b => b.Match(c => c.Field(d => d.f_itemid).Query(itemId)))));
            }
            else {
                return null;
            }

            if (!string.IsNullOrEmpty(keyword))
            {
                keyword = keyword.Trim();

                searchDescriptor.Query(q => q.Bool(a => a.Must(b => b.MultiMatch(c => c.Fields(d => d.Field(e => e.f_itemname).Field(e => e.f_itemcode).Field(e => e.f_description)).Query(keyword))))).Highlight(b => b.Fields(
                               f => f.Field(g => g.f_itemname),
                               l => l.Field(g => g.f_description))
                          .PreTags("<font size='3' color='red'>")
                          .PostTags("</font>")
             );
            }
            searchDescriptor.From((pagination.page-1)*pagination.rows).Size(pagination.rows);
            ISearchResponse<SysExptEntityModel> searchResults = client.Search<SysExptEntityModel>(searchDescriptor);
            var list = searchResults.Hits.Select(c => new SysExptEntity
            {
                F_ItemName = c.Highlight == null || !c.Highlight.Keys.Contains("f_itemname") ? c.Source.f_itemname : string.Join("", c.Highlight["f_itemname"]),
                F_Id = c.Source.f_id,
                F_CreatorTime = c.Source.f_creatortime,
                F_CreatorUserId = c.Source.f_creatoruserid,
                F_DeleteMark = c.Source.f_deletemark,
                F_DeleteTime = c.Source.f_deletetime,
                F_DeleteUserId = c.Source.f_deleteuserid,
                F_Description = c.Source.f_description,
                F_EnabledMark = c.Source.f_enabledmark,
                F_IsDefault = c.Source.f_isdefault,
                F_ItemCode = c.Source.f_itemcode,
                F_ItemId =c.Source.f_itemid,
                F_LastModifyTime = c.Source.f_lastmodifytime,
                F_LastModifyUserId = c.Source.f_lastmodifyuserid,
                F_Layers = c.Source.f_layers,
                F_ParentId = c.Source.f_parentid,
                F_SimpleSpelling = c.Source.f_simplespelling,
                F_SortCode =c.Source.f_sortcode
                
            }) ;
            List<SysExptEntity> res = new List<SysExptEntity>();
            res = list.ToList();
             
            return  res;
        }
        public List<SysExptEntity> GetExptTerm(string name)
        {

            ISearchResponse<SysExptEntity> searchResults = client.Search<SysExptEntity>(s => s.Query(b =>
                b.MultiMatch(c => c
                    .Query(name)
                    .Fields(d => d.Field(f => f.F_ItemName)
                        .Field(f => f.F_Description)
                    )
                  )
                )
            .Highlight(b => b.Fields(
                    f => f.Field(g => g.F_ItemName),
                         l => l.Field(g => g.F_Description)

                )
                    .PreTags("<b>")
                    .PostTags("</b>")
             )
            );
            List<string> test = new List<string>();
            var list = searchResults.Hits.Select(c => new HightLine
            {
                itemName = c.Highlight == null || !c.Highlight.Keys.Contains("f_itemname") ? c.Source.F_ItemName : string.Join("", c.Highlight["f_itemname"]),
                descript = c.Highlight == null || !c.Highlight.Keys.Contains("f_description") ? c.Source.F_Description : string.Join("", c.Highlight["f_description"]),
            });
            List<HightLine> res = new List<HightLine>();
            res = list.ToList();
            searchResults.Hits.Select(c => test = c.Highlight.Keys.ToList());
            return searchResults.Documents.ToList();
        }
        public List<PersonDetail> GetAll(string name)
        {
            ISearchResponse<PersonDetail> searchResults = client.Search<PersonDetail>(s =>
            s.Query(
                    q => q.QueryString(a =>
                         a.Fields(b => b.Field(c => c.firstName).Field(c => c.lastName))
                         .Query(name)
                      )
                ));
            return searchResults.Documents.ToList();
        }
    }
    public class HightLine
    {
        public string itemName { get; set; }
        public string descript { get; set; }
    }
    public partial class PersonDetail
    {
        public long id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
    }
}
