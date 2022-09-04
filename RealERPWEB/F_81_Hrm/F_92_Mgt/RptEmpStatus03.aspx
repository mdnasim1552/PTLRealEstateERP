<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptEmpStatus03.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_92_Mgt.RptEmpStatus03" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register Assembly="DropCheck" Namespace="xMilk" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 
     <script src="../../Scripts/gridviewScrollHaVertworow.min.js"></script>
    <link href="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/css/select2.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/js/select2.min.js"></script>
   
   <script language="javascript" type="text/javascript">
       $(document).ready(function () {
           //For navigating using left and right arrow of the keyboard
           Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
           $('#<%=this.txtSrcCompany.ClientID %>').focus();

       });
       $(document).ready(function () {
           $(".select2").select2();
           Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

       });
       function pageLoaded() {

           $("input, select").bind("keydown", function (event) {
               var k1 = new KeyPress();
               k1.textBoxHandler(event);
           });
           $('.select2').each(function () {
               var select = $(this);
               select.select2({
                   placeholder: 'Select an option',
                   width: '100%',
                   allowClear: !select.prop('required'),
                   language: {
                       noResults: function () {
                           return "{{ __('No results found') }}";
                       }
                   }
               });
           });
          
       };
   </script>
  
    

    <style>
      .chzn-drop {
            width: 100% !important;
        }
        .chzn-container{
            width: 100% !important;
        }

        .chzn-container-single .chzn-single {
            height: 28px !important;
            line-height: 28px !important;
        }

        .GridViewScrollHeader TH, .GridViewScrollHeader TD {
            font-weight: normal;
            white-space: nowrap;
            border-right: 1px solid #e6e6e6;
            border-bottom: 1px solid #e6e6e6;
            background-color: #F4F4F4;
            color: #999999;
            text-align: left;
            vertical-align: bottom;
        }

        .GridViewScrollItem TD {
            white-space: nowrap;
            border-right: 1px solid #e6e6e6;
            border-bottom: 1px solid #e6e6e6;
            background-color: #FFFFFF;
            color: #444444;
        }

        .GridViewScrollItemFreeze TD {
            white-space: nowrap;
            border-right: 1px solid #e6e6e6;
            border-bottom: 1px solid #e6e6e6;
            background-color: #FAFAFA;
            color: #444444;
        }

        .GridViewScrollFooterFreeze TD {
            white-space: nowrap;
            border-right: 1px solid #e6e6e6;
            border-top: 1px solid #e6e6e6;
            border-bottom: 1px solid #e6e6e6;
            background-color: #F4F4F4;
            color: #444444;
        }
    </style>
 
 
 
 
        
                
                
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="RealProgressbar">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
                    <ProgressTemplate>
                        <div id="loader">
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="lading"></div>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>

             <div class="card mt-5">
                <div class="card-header">
                    
                    <div class="row mt-1">
                       

                                
                               
                                    <div class="col-lg-2 col-md-2 col-sm-6">
                                        <asp:Label ID="Label2" runat="server" CssClass="form-label">Company</asp:Label>
                                        <asp:TextBox ID="txtSrcCompany" runat="server" CssClass="form-control d-none"></asp:TextBox>
                                        <asp:LinkButton ID="imgbtnCompany" runat="server" OnClick="imgbtnCompany_Click"><i class="fas fa-search"></i></asp:LinkButton>
                                    
                                   
                                        <asp:DropDownList ID="ddlCompany" runat="server"  CssClass="form-control select2 pull-left mr-3" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                                        </asp:DropDownList>


                                    </div>

                                
                               
                                    <div class="col-lg-2 col-md-2 col-sm-6">
                                        <asp:Label ID="Label1" runat="server" CssClass="form-label">Grade</asp:Label>
                                        <asp:TextBox ID="txtSrcgrade" runat="server" CssClass="form-control d-none"></asp:TextBox>
                                        <asp:LinkButton ID="imgbtngrade" runat="server" OnClick="imgbtngrade_Click"><i class="fas fa-search"></i></asp:LinkButton>
                                   
                                        <asp:DropDownList ID="ddlgrade" runat="server"  OnSelectedIndexChanged="ddlgrade_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control select2" TabIndex="6">
                                        </asp:DropDownList>

                                    </div>

                               
                               
                                    <div class="col-lg-2 col-md-2 col-sm-6">
                                        <asp:Label ID="lblDept" runat="server" CssClass="form-label">Department</asp:Label>
                                        <asp:TextBox ID="txtSrcPro" runat="server" CssClass="form-control d-none"></asp:TextBox>
                                        <asp:LinkButton ID="imgbtnDeptSrch" runat="server" OnClick="imgbtnProSrch_Click"><i class="fas fa-search"></i></asp:LinkButton>
                                    
                                        <asp:DropDownList ID="ddlDepartment" runat="server"  OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control select2" TabIndex="6">
                                        </asp:DropDownList>
                                        
                                    </div>

                               
                                
                                    <div class="col-lg-3 col-md-3 col-sm-6">
                                        <asp:TextBox ID="txtSrcSec" runat="server" CssClass="form-control d-none"></asp:TextBox>
                                        <asp:Label ID="lblSection" runat="server">Section
                                        <asp:LinkButton ID="imgbtnSecSrch" runat="server"  OnClick="imgbtnSecSrch_Click"><i class="fas fa-search"></i></asp:LinkButton>
                                        </asp:Label>


                                        <asp:ListBox ID="DropCheck1" runat="server" CssClass="form-control select2" SelectionMode="Multiple"></asp:ListBox>
                                        
                                    </div>

                              
                                
                               
                                    <div class="col-lg-1 col-md-1 col-sm-6">
                                        <asp:Label ID="lblPage" runat="server" CssClass="form-label">Page Size</asp:Label>
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>100</asp:ListItem>
                                            <asp:ListItem>150</asp:ListItem>
                                            <asp:ListItem>200</asp:ListItem>
                                            <asp:ListItem>300</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--<asp:Label ID="lblmsg" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>--%>
                                    </div>

                                    
                        <div class="col-lg-1 col-md-1 col-sm-6">
                                
                         <asp:LinkButton ID="lnkbtnShow" runat="server" CssClass="btn btn-primary okBtn pull-left mt-3" OnClick="lnkbtnShow_Click">Ok</asp:LinkButton>
                            </div>
                    </div>

           
                        
                    <div class="row mt-3">
                        <asp:GridView ID="gvEmpList" runat="server" AllowPaging="True"  CssClass="table-striped table-hover table-bordered grvContentarea"
                            AutoGenerateColumns="False" OnPageIndexChanging="gvEmpList_PageIndexChanging" 
                            ShowFooter="True" Width="420px">
                           
                            <Columns>
                                <asp:TemplateField HeaderText="Sl #">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px" 
                                            Style="text-align: right" 
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                              
                                <asp:TemplateField HeaderText="Card #">
                                   
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvcardnoemp" runat="server" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>' 
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                               <asp:TemplateField HeaderText="Company &amp; Employee Name">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblgvcompanyandemp" runat="server" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "companyname")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "gradedesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "companyname")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         "<B>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "gradedesc")).Trim()+"</B>": "")  + 
                                                                         (DataBinder.Eval(Container.DataItem, "empname").ToString().Trim().Length>0 ? 
                                                                          (Convert.ToString(DataBinder.Eval(Container.DataItem, "gradedesc")).Trim().Length>0 ?   "<br>" :"") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")).Trim(): "")
                                                                    %>' Width="300px">
                                                    
                                                    
                                                    
                                                    </asp:Label>
                                                     </ItemTemplate>
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                     <HeaderStyle HorizontalAlign="left" VerticalAlign="Middle" />
                                                 </asp:TemplateField>
                                 
                                
                                <asp:TemplateField HeaderText="Designation">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvdesignationemp" runat="server" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>' 
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>


                                  <asp:TemplateField HeaderText="Department">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblgvDeptname" runat="server" 
                                                             Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>' 
                                                             Width="150px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                 </asp:TemplateField>
                                <asp:TemplateField HeaderText="Joining Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvjoindateemp" runat="server" BackColor="Transparent" 
                                            BorderStyle="None" Font-Size="11px" 
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "joindate")).ToString("dd-MMM-yyyy") %>' 
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Service Period">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblgvserperiod" runat="server" 
                                                             Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "serperiod")) %>' 
                                                             Width="150px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                 </asp:TemplateField>



                                                   <asp:TemplateField HeaderText="Salary">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lgvsalary" runat="server" style="text-align: right" 
                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "gssal")).ToString("#,##0;(#,##0); ") %>' 
                                                             Width="70px"></asp:Label>
                                                     </ItemTemplate>
                                                     <FooterStyle HorizontalAlign="Right" />
                                                     <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                     <ItemStyle HorizontalAlign="right" />
                                                 </asp:TemplateField>
                             
                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>
                   </div>

                    </div>
                    </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

