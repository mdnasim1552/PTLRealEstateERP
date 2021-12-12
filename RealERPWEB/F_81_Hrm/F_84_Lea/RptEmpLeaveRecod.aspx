<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="RptEmpLeaveRecod.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_84_Lea.RptEmpLeaveRecod" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <script type="text/javascript">
         $(document).ready(function () {
             Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

         });

         function pageLoaded() {
             //$('.datepicker').datepicker({
             //    format: 'mm/dd/yyyy',
             //});
             $("input, select").bind("keydown", function (event) {
                 var k1 = new KeyPress();
                 k1.textBoxHandler(event);
             });

             $(".chosen-select").chosen({
                 search_contains: true,
                 no_results_text: "Sorry, no match!",
                 allow_single_deselect: true
             });


             $('.chzn-select').chosen({ search_contains: true });

         };
         
     </script>
    <style>
         .chzn-container-single {
            width: auto !important;
            height: 34px !important;
        }

            .chzn-container-single .chzn-single {
                height: 36px !important;
                line-height: 36px;
            }

        /*  .project-slect  .chzn-container-single{
         width: 100px !important;
            height: 34px !important;
        
        }*/
        .profession-slect .chzn-container-single {
            height: 34px !important;
        }
    </style>



    <div class="card card-fluid container-data mt-5" id='printarea'>
        <div class="card-body" style="min-height: 600px;">
           
                 <div class="row mb-2">
                <div class="col-md-4 p-0">
                    <div class="input-group input-group-alt">
                        <div class="input-group-prepend ">
                            <button class="btn btn-secondary" type="button">From</button>
                        </div>
                        <asp:TextBox ID="txtfodate" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                            Format="dd-MMM-yyyy" TargetControlID="txtfodate"></cc1:CalendarExtender>
                        <div class="input-group-prepend">
                            <button class="btn btn-secondary" id="lblToDate" runat="server" type="button">To</button>
                        </div>
                        <asp:TextBox ID="txttodate" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                        <cc1:CalendarExtender ID="Cal3" runat="server"
                            Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>

                    </div>
                </div>

                <div class="col-md-3 p-0">
                    <div class="input-group input-group-alt">
                        <div class="input-group-prepend">
                            <button class="btn btn-secondary ml-1" type="button">Company</button>
                        </div>
                        <asp:DropDownList ID="ddlcomp" ClientIDMode="Static" data-placeholder="Choose Company" runat="server" CssClass="custom-select chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlcomp_SelectedIndexChanged">
                        </asp:DropDownList>

                    </div>
                </div>

                <div class="col-md-3 p-0">
                    <div class="input-group input-group-alt">
                        <div class="input-group-prepend">
                            <button class="btn btn-secondary" type="button">Department</button>
                        </div>
                        <asp:DropDownList ID="ddldept" ClientIDMode="Static" data-placeholder="Choose Department" runat="server" CssClass="custom-select chzn-select " AutoPostBack="true" OnSelectedIndexChanged="ddldept_SelectedIndexChanged">
                        </asp:DropDownList>

                    </div>
                </div>

                <div class="col-md-2 p-0">
                    <div class="input-group input-group-alt">
                        <div class="input-group-prepend">
                            <button class="btn btn-secondary pl-0 pr-0" type="button">Section</button>
                        </div>
                        <asp:DropDownList ID="ddlsec" data-placeholder="Choose Section" runat="server" CssClass="custom-select chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlsec_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4 p-0 mt-2">
                    <div class="input-group input-group-alt profession-slect">
                        <div class="input-group-prepend">
                            <button class="btn btn-secondary" type="button">Employee</button>
                        </div>
                        <asp:DropDownList ID="ddlEmp" data-placeholder="Choose Employee" runat="server" CssClass="custom-select chzn-select" AutoPostBack="true">
                        </asp:DropDownList>
                        <div class="input-group-prepend">
                            <asp:LinkButton ID="lnkok" runat="server" Text="Ok" OnClick="lnkok_Click" CssClass="btn btn-primary okBtn">Ok</asp:LinkButton>

                        </div>
                    </div>
                </div>
                <div class="col-md-2 p-0 mt-2 pading5px">
                    <div class="input-group input-group-alt">
                        <div class="input-group-prepend">
                            <button class="btn btn-secondary" type="button">Page</button>
                        </div>
                        <asp:DropDownList ID="ddlpage" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlpage_SelectedIndexChanged">
                            <asp:ListItem>10</asp:ListItem>
                            <asp:ListItem>20</asp:ListItem>
                            <asp:ListItem>20</asp:ListItem>
                            <asp:ListItem>30</asp:ListItem>
                            <asp:ListItem>50</asp:ListItem>
                            <asp:ListItem>100</asp:ListItem>
                            <asp:ListItem>150</asp:ListItem>
                            <asp:ListItem>200</asp:ListItem>
                            <asp:ListItem>300</asp:ListItem>
                            <asp:ListItem>400</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

            </div>
            </div>
            <div class="clearfix"></div>
          
            <hr />
            <div class="row">
                <div class="table-responsive">
                                    <asp:GridView ID="gvLeavRecod" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    OnPageIndexChanging="gvLeavRecod_PageIndexChanging" ShowFooter="True"  CssClass="table-striped table-hover table-bordered grvContentarea"
                                    PageSize="15">
                                    <PagerSettings Position="Top" />
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Section">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSection" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "secname")) %>'
                                                    Width="180px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Emp. ID">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvempid" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ID CARD">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvidcard" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                                    Width="55px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Employee Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvEmpName" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                                    Width="220px"></asp:Label>
                                            </ItemTemplate>
                                            
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>



                                         <asp:TemplateField HeaderText="Join Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvjoindate" runat="server" Height="16px" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "joindate")).ToString("dd-MMM-yyyy") %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDesig" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                          <asp:TemplateField HeaderText="Opening Leave">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvoplv" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    ForeColor="#000" Style="font-size: 11px; text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "oplv")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Earned Leave">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvel" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    ForeColor="#000" Style="font-size: 11px; text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ernleave")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Casual Leave">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvcl" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    ForeColor="#000" Style="font-size: 11px; text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "csleave")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sick Leave">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvsl" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    ForeColor="Black" Style="font-size: 11px; text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "skleave")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Enjoyee Casual Leave">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtenjcaslv" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    ForeColor="Black" Style="font-size: 11px; text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cleavenj")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                          <asp:TemplateField HeaderText="Enjoyee Sick Leave">
                                            <ItemTemplate>
                                                <asp:TextBox ID="sickleavenj" runat="server" BackColor="Transparent" BorderStyle="None"
                                                    ForeColor="Black" Style="font-size: 11px; text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cleavenj")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="70px"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" />
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
            <div class="clearfix"></div>
            <br />
        
    </div>

</asp:Content>
