<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="InterfaceApprovalForm.aspx.cs" Inherits="RealERPWEB.F_99_Allinterface.InterfaceApprovalForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });

            $('.chzn-select').chosen({ search_contains: true });

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
            <div class="card-fluid container-data  mt-2">
                       <div class="card">
                                <div class="card-header bg-light">
                                                              <div>
                                        <asp:RadioButtonList ID="viewType" runat="server" AutoPostBack="True"
                                            CssClass="custom-control custom-control-inline custom-checkbox rbt p-0"
                                            Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                            OnSelectedIndexChanged="viewType_SelectedIndexChanged"
                                            RepeatDirection="Horizontal">
                          
                                            <asp:ListItem>Leave </asp:ListItem>
                                            <asp:ListItem>Loan</asp:ListItem>
                                            <asp:ListItem>AI</asp:ListItem>
                         

                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                           </div>

                <asp:Panel runat="server" ID="pnlLeave">
                    <div class="row">
                        <div class="col-12 col-lg-12 col-xl-3">
                            <section class="card card-fluid" style="height: 650px">
                                <!-- .card-body -->
                                <header class="card-header">HR APPROVAL SETUP</header>
                                <div class="card-body">
                                    <div class="form-group row">
                                        <label for="ddlLvType" class="col-md-12">
                                            Role Of Type
                                        
                                        </label>
                                        <asp:DropDownList ID="ddlTypeRole" runat="server" CssClass="form-control inputTxt chzn-select">
                                            <asp:ListItem Value="DPT">Department Team</asp:ListItem>
                                            <asp:ListItem Value="MGT">Management Team</asp:ListItem>
                                        </asp:DropDownList>

                                    </div>
                                    <div class="form-group row">
                                        <label for="ddlLvType" class="col-md-12">
                                            Department / Section Head
                                        <asp:LinkButton ID="lbtnOkOrNew" runat="server" CssClass="btn btn-primary btn-sm float-right okBtn" OnClick="GetUserInfo">Ok</asp:LinkButton>
                                        </label>
                                        <asp:DropDownList ID="ddldpt" runat="server" CssClass="form-control inputTxt chzn-select">
                                        </asp:DropDownList>

                                    </div>
                                    <asp:Panel ID="Panel2" runat="server" Visible="False">
                                        <div class="form-group row">
                                            <label for="ddlLvType">
                                                User Name
                                            </label>
                                            <asp:DropDownList ID="ddlUserList" runat="server" CssClass="form-control inputTxt chzn-select">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-6 pl-0">
                                            </div>
                                            <div class="col-md-6 pl-0">
                                                <div class="form-group">

                                                    <asp:LinkButton ID="lbtnSelect" runat="server" CssClass="btn btn-sm btn-primary okBtn" OnClick="Select_Click">Select</asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnSelectAll" runat="server" Visible="false" CssClass="btn  btn-sm btn-primary okBtn" OnClick="SelectAll_Click">Select All</asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </section>
                        </div>
                        <div class="col-12 col-lg-12 col-xl-9">
                            <section class="card card-fluid mb-0" style="height: 650px; flex-grow: 1; overflow: auto;">
                                <!-- .card-body -->
                                <header class="card-header">Users List</header>

                                <div class="card-body card card-fluid mb-0">
                                    <div class="row" style="height: 180px; flex-grow: 1; overflow: auto;">
                                        <asp:GridView ID="gvProLinkInfo" runat="server" CssClass="table-striped table-hover table-bordered grvContentarea"
                                            AutoGenerateColumns="False" ShowFooter="True"
                                            OnRowDeleting="gvProLinkInfo_RowDeleting">
                                            <PagerSettings Visible="False" />
                                            <RowStyle />
                                            <Columns>
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo0" runat="server"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:CommandField ShowDeleteButton="True" DeleteText='<i class="fa fa-trash "></i>' />

                                                <asp:TemplateField HeaderText="Sl.No." Visible="true">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="slno" runat="server" Height="16px" Style="text-align: center;"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "slno")) %>'
                                                            Width="30px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="User ID" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbltyprole" runat="server" Visible="false"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "roletype")) %>'></asp:Label>
                                                        <asp:Label ID="lblcentrid" runat="server" Visible="false"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "centrid")) %>'></asp:Label>
                                                        <asp:Label ID="userid" runat="server" Height="16px" Style="text-align: center;"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrid")) %>'
                                                            Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lbtnDeleteAll" runat="server" Font-Bold="True"
                                                            Font-Size="13px" Height="16px" OnClick="lbtnDeleteAll_Click"
                                                            Width="100px">Delete All</asp:LinkButton>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />

                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="User NAME">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSuplDesc1" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrsname")) %>'
                                                            Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lbtnUpdate" runat="server" Font-Bold="True"
                                                            Font-Size="13px" OnClick="lbtnUpdate_Click"
                                                            Style="text-align: center; height: 15px;" Width="150px">Final Update</asp:LinkButton>
                                                    </FooterTemplate>
                                                    <FooterStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Department">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSuplDesc2" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                            Width="150px"></asp:Label>
                                                    </ItemTemplate>


                                                    <FooterStyle HorizontalAlign="left" />
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <EditRowStyle />
                                            <AlternatingRowStyle />
                                        </asp:GridView>
                                    </div>

                                </div>
                            </section>
                        </div>
                    </div>
                </asp:Panel>

                <asp:Panel runat="server" ID="pnlLoan">
                    <div class="row">
                        <div class="col-12 col-lg-12 col-xl-3">
                            <section class="card card-fluid" style="height: 650px">
                                <!-- .card-body -->
                                <header class="card-header">Loan Step Permission</header>
                                <div class="card-body">
                                    <div class="form-group row">
                                        <label for="ddlLvType" class="col-md-12">
                                            Loan Step
                                        
                                        </label>
                                        <asp:DropDownList ID="ddlLoanStep" runat="server" CssClass="form-control inputTxt chzn-select">
                                        </asp:DropDownList>

                                    </div>


                                    <div class="form-group row">
                                        <label for="ddlLvType">
                                            User Name
                                        </label>
                                        <asp:DropDownList ID="ddlLoanUser" runat="server" CssClass="form-control inputTxt chzn-select">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="row">
                                        <asp:LinkButton ID="saveBtnLn" runat="server" CssClass="btn btn-primary btn-sm float-right okBtn" OnClick="saveBtnLn_Click">Ok</asp:LinkButton>
                                    </div>

                                </div>
                            </section>
                        </div>
                        <div class="col-12 col-lg-12 col-xl-9">
                            <section class="card card-fluid mb-0" style="height: 650px; flex-grow: 1; overflow: auto;">
                                <!-- .card-body -->
                                <header class="card-header">Users List</header>

                                <div class="card-body card card-fluid mb-0">
                                    <div class="row" style="height: 180px; flex-grow: 1; overflow: auto;">
                                        <asp:GridView ID="gvLoanStep" runat="server" CssClass="table-striped table-hover table-bordered grvContentarea"
                                            AutoGenerateColumns="False" ShowFooter="True">
                                            <PagerSettings Visible="False" />
                                            <RowStyle />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblnid" runat="server"
                                                          
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                               

                                                <asp:TemplateField HeaderText="Step Name" Visible="true">
                                                    <ItemTemplate>
                                                                            <asp:Label ID="lblLid" runat="server"  Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "id")) %>' Width="35px" Visible="false"></asp:Label>
                                                     <asp:Label ID="lblstpname" runat="server"  Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "stepname")) %>' Width="200px"></asp:Label>
                                                    </ItemTemplate>
                                        
                                                </asp:TemplateField>

                                                
                                                <asp:TemplateField HeaderText="User Name" Visible="true">
                                                    <ItemTemplate>
                                                     <asp:Label ID="lbllnuser" runat="server"  Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrname")) %>' Width="200px" ></asp:Label>
                                                    </ItemTemplate>
                                      
                                                </asp:TemplateField>

                                                          <asp:TemplateField HeaderText="Action" Visible="true">
                                                    <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="btbRemove" OnClick="btnRemove_Click" CssClass="text-danger"><i class="fa fa-trash"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                           
                                                </asp:TemplateField>




                                            </Columns>
                                            <EditRowStyle />
                                            <AlternatingRowStyle />
                                        </asp:GridView>
                                    </div>

                                </div>
                            </section>
                        </div>

                    </div>

                </asp:Panel>
            </div>


            <script src="http://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/js/bootstrap-multiselect.js"
                type="text/javascript"></script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
