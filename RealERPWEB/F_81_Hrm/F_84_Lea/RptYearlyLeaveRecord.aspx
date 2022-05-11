<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptYearlyLeaveRecord.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_84_Lea.RptYearlyLeaveRecord" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        .mt20 {
            margin-top: 20px;
        }

        .chzn-container {
            width: 100% !important;
        }

        .chzn-drop {
            width: 100% !important;
        }

        .chzn-container-single .chzn-single {
            height: 28px !important;
            line-height: 28px !important;
        }

        .card-body {
            min-height: 400px !important;
        }

        .pd4 {
            padding: 4px !important;
        }
    </style>

    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });


        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });
            var gvEmpListTrans = $('#<%=this.gvEmplvStatus.ClientID %>');
            gvEmpListTrans.Scrollable();

            $('.chzn-select').chosen({ search_contains: true });

        };

        function filter2(phrase, _id) {
            var words = phrase.value.toLowerCase().split(" ");
            var table = document.getElementById(_id);
            var ele;
            for (var r = 1; r < table.rows.length; r++) {
                ele = table.rows[r].innerHTML.replace(/<[^>]+>/g, "");
                var displayStyle = 'none';
                for (var i = 0; i < words.length; i++) {
                    if (ele.toLowerCase().indexOf(words[i]) >=0)
                        displayStyle = '';
                    else {
                        displayStyle = 'none';
                        break;
                    }
                }
                table.rows[r].style.display = displayStyle;
            }
        }


    </script>







    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="RealProgressbar">
                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
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
                    <div class="row">
                        <div class="col-lg-3">
                             <div class="form-group">
                                                       <asp:Label ID="Label4" runat="server">Company</asp:Label>
                  
                                        <asp:DropDownList ID="ddlCompany" runat="server"  CssClass="form-control chzn-select"  OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                                        </asp:DropDownList>
                             </div>
                     
                        </div>
                        <div class="col-lg-3">
                               <div class="form-group">
                                                     <asp:Label ID="lblDept" runat="server">Department</asp:Label>
                  
                                        <asp:DropDownList ID="ddlProjectName" runat="server" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control chzn-select" TabIndex="6">
                                        </asp:DropDownList>
                               </div>
                     
                        </div>
                        <div class="col-lg-3">
                               <div class="form-group">
                                                                  <asp:Label ID="Label3" runat="server" >Section</asp:Label>
                          
                                        <asp:DropDownList ID="ddlSection" runat="server"  CssClass="form-control  chzn-select" TabIndex="6" AutoPostBack="true">
                                        </asp:DropDownList>
                               </div>
          
                        </div>

                        <div class="col-lg-1">
                            <div class="form-group">
                                         <asp:Label ID="lblPage" runat="server" >Year</asp:Label>
                                        <asp:DropDownList ID="ddlyear" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm">
                                            <asp:ListItem Value="2017">2017</asp:ListItem>
                                            <asp:ListItem Value="2018">2018</asp:ListItem>
                                            <asp:ListItem Value="2019">2019</asp:ListItem>
                                            <asp:ListItem Value="2020">2020</asp:ListItem>
                                            <asp:ListItem Value="2021">2021</asp:ListItem>
                                            <asp:ListItem Value="2022">2022</asp:ListItem>
                                            <asp:ListItem Value="2023">2023</asp:ListItem>



                                            
                                        </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-1">
                            <div class="form-group">
                                        <asp:Label ID="Label1" runat="server" >ID Card</asp:Label>
                                <input id="txtTerm" name="txtTerm" onkeyup="filter2(this, '<%=gvEmplvStatus.ClientID %>')" type="text"  class="form-control form-control-sm">
                            </div>
                        </div>
                        <div class="col-lg-1">
                               <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm mt20" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                        </div>
                       
                    </div>
                </div>
                <div class="card-body">
                           <asp:GridView ID="gvEmplvStatus" runat="server" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    AutoGenerateColumns="False"
                                    ShowFooter="True" >
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: center"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        
                                    <asp:TemplateField HeaderText="Company Name">

                                             <HeaderTemplate>

                                                         <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                                        Text="Company Name" Width="150px"></asp:Label>


                                                        <asp:HyperLink ID="hlbtntbCdataExcelemplist" runat="server"
                                                                        CssClass="btn  btn-success btn-xs" ToolTip="Export Excel"><i  class=" fa fa-file-excel-o "></i></asp:HyperLink>
                                                     
                                                    </HeaderTemplate>

                                            <ItemTemplate>
                                                <asp:Label ID="lgvcustname" runat="server"
                                                    Text='<%# "<B>" +Convert.ToString(DataBinder.Eval(Container.DataItem, "companyname"))+"<B>" %>'
                                                    Width="200px"></asp:Label>
                                            </ItemTemplate>
                                             <FooterTemplate>
                                                <asp:Label ID="lgvTotalnagad" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>


                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField HeaderText="Company Name">



                                            <ItemTemplate>
                                                <asp:Label ID="lblgvCompanynameemp" runat="server"
                                                    Text='<%# "<B>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "companyname"))  +"<B>"  %>'
                                                    Width="150px">                                                                        
                                              
                                                </asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                        </asp:TemplateField>--%>

                                        <asp:TemplateField HeaderText="Department Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvdepname" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptdesc")) %>'
                                                    Width="130px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                          <asp:TemplateField HeaderText="Section Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvsectionname" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sectionname")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                            <asp:LinkButton ID="btnTotal" runat="server" OnClick="btnTotal_Click" CssClass="btn btn-primary btn-sm" >Total</asp:LinkButton>
                                        </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
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

                                         <asp:TemplateField HeaderText="Employee Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvempname" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                                    Width="160px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterTemplate>
                                            <asp:LinkButton ID="lbnLvRecordUpdate" runat="server" OnClick="lbnLvRecordUpdate_Click" CssClass="btn btn-success btn-sm"  >Update</asp:LinkButton>
                                        </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        
                                       
                                        <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvdesignationemp" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Joining Date">

                                             <ItemTemplate>
                                                    <asp:Label ID="lbljoindate" runat="server" Style="text-align: left"
                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "joindat")).ToString("dd-MMM-yyyy") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                            
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                          <asp:TemplateField HeaderText="Carry Foward">
                                           
                                                 <ItemTemplate>
                                                    <asp:Label ID="lblOpnlv" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnlv")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                        Width="60px"></asp:Label >
                                                </ItemTemplate>
                                                                                        
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Leave Allocation">
                                           
                                                 <ItemTemplate>
                                                    <asp:TexTBox ID="txtallocation" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "earnlv")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                        Width="60px"></asp:TexTBox>
                                                </ItemTemplate>
                                                                                        
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        
                                          <asp:TemplateField HeaderText="Exercise">
                                           
                                             <ItemTemplate>
                                                    <asp:TexTBox ID="txtExercise" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "enjoylv")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                        Width="60px"></asp:TexTBox>
                                                </ItemTemplate> 
                                              
                                              
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                          <asp:TemplateField HeaderText="Balance">

                                               <ItemTemplate>
                                                    <asp:TexTBox ID="txtbalance" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ballv")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                        Width="60px"></asp:TexTBox>
                                                </ItemTemplate> 
                                        
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                                                                
                                           <asp:TemplateField HeaderText="Carry Forward">
                                              <ItemTemplate>
                                                    <asp:TexTBox ID="txtcarryfoward" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "carrfor")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                        Width="60px"></asp:TexTBox>
                                                </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
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

<%--            <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                
                               


                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label4" runat="server" CssClass="lblTxt lblName">Company</asp:Label>
                                        <asp:TextBox ID="txtSrcCompany" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="imgbtnCompany" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnCompany_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-4 asitCol4 pading5px">
                                        <asp:DropDownList ID="ddlCompany" runat="server"  CssClass="form-control inputTxt chzn-select"  OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" AutoPostBack="true" TabIndex="2">
                                        </asp:DropDownList>

                                  

                                    </div>
                                     <div class="col-md-1 asitCol1 pading5px">
                                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn pull-left" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                                        </div>

                                </div>



                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblDept" runat="server" CssClass="lblTxt lblName">Department</asp:Label>
                                        <asp:TextBox ID="txtSrcPro" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="ibtnFindProject" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ibtnFindProject_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-3 pading5px asitCol4">
                                        <asp:DropDownList ID="ddlProjectName" runat="server" Width="336px" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control inputTxt chzn-select" TabIndex="6">
                                        </asp:DropDownList>

                                    </div>

                                </div>

                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label3" runat="server" CssClass="lblTxt lblName">Section</asp:Label>
                                        <asp:TextBox ID="txtSrcSec" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="imgbtnSecSrch" runat="server" CssClass="btn btn-primary srearchBtn"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                    </div>
                                    <div class="col-md-4 pading5px asitCol4">
                                        <asp:DropDownList ID="ddlSection" runat="server"  CssClass="form-control inputTxt chzn-select" TabIndex="6" AutoPostBack="true">
                                        </asp:DropDownList>

                                        <cc1:ListSearchExtender ID="ddlSection_ListSearchExtender" runat="server"
                                            QueryPattern="Contains" TargetControlID="ddlSection">
                                        </cc1:ListSearchExtender>
                                                                         <asp:Label ID="lblSectionDesc" runat="server" CssClass="form-control inputTxt" Visible="False" Width="233"></asp:Label>
                                    </div>

                                </div>


                                   <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblPage" runat="server" CssClass="lblTxt lblName">Year</asp:Label>
                                        <asp:DropDownList ID="ddlyear" runat="server" AutoPostBack="True" CssClass="ddlPage">
                                            <asp:ListItem Value="2017">2017</asp:ListItem>
                                            <asp:ListItem Value="2018">2018</asp:ListItem>
                                            <asp:ListItem Value="2019">2019</asp:ListItem>
                                            <asp:ListItem Value="2020">2020</asp:ListItem>
                                            <asp:ListItem Value="2021">2021</asp:ListItem>
                                            <asp:ListItem Value="2022">2022</asp:ListItem>
                                            <asp:ListItem Value="2023">2023</asp:ListItem>



                                            
                                        </asp:DropDownList>
                                    </div>

                                    

                                </div>


                                
                             <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblPage" runat="server" CssClass="lblTxt lblName">Page Size</asp:Label>
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
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
                                    </div>

                                    

                                </div>

                            </div>
                        </fieldset>--%>
                     

                        
                
                            
 
                           

                          
                               
                         
                            
                

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
