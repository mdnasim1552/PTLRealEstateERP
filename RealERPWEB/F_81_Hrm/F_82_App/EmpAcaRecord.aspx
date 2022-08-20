<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="EmpAcaRecord.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_82_App.EmpAcaRecord" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

            });
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


            <div class="container-data mt-5">



            


                    <div class="row">
                        <div class="col-md-3 col-lg-3 col-sm-3">
                            <div class="card">
                                <div class="card-header">Add description</div>
                                <div class="card-body">
                                <div class="from-group">
                                    <asp:Label ID="Label1" runat="server"> Select Title</asp:Label>
                                <asp:DropDownList ID="ddlEmpAcarecord" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlEmpAcarecord_SelectedIndexChanged" AutoPostBack="True" TabIndex="3"></asp:DropDownList>
                            </div>


                            <div class="from-group">
                                    <asp:Label ID="Label2" runat="server">New Title</asp:Label>
                                     <asp:TextBox ID="txtBoxTitle" runat="server" CssClass="form-control"></asp:TextBox>
                              </div>



                          
                                    <asp:HiddenField ID="editbyId" runat="server" />
                                    <p class="text-right"> <asp:LinkButton ID="lnkAdd" runat="server" Text="Submit" OnClick="lnkAdd_Click" CssClass="btn btn-success btn-sm mt-3"></asp:LinkButton></p>
                                   
                          
                   
                     
                                </div>
                            </div>
                        </div>

                        <div class="col-md-9 col-lg-9 col-sm-9">
                            <div class="card">
                                <div class="card-header">Description</div>
                                <div class="card-body">
                                    <asp:GridView ID="grvacc" runat="server" AllowPaging="True"
                                AutoGenerateColumns="False" BorderColor="SteelBlue" BorderStyle="Solid"
                                CssClass="table-striped table-hover table-bordered grvContentarea"                                 
                                  ShowFooter="True" Width="600px"
                                PageSize="15">
                                <PagerSettings NextPageText="Next" PreviousPageText="Previous"
                                    Visible="False" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblserialnoid" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="14px" />
                                        <ItemStyle Font-Size="12px" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText=" ">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgrcode" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "subcode2"))+"-" %>'
                                                Width="20px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="12px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtgrcode" runat="server" Font-Size="12px" Height="16px"
                                                MaxLength="7"
                                                Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "subcode3")) %>'
                                                Width="50px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbgrcod3" runat="server" Font-Size="12px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "subcode3")) %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="14px" />
                                        <ItemStyle Font-Size="12px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description of Code">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtgvDesc" runat="server" Font-Size="12px" MaxLength="100"
                                                Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "subdesc")) %>'
                                                Width="250px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="ddlPageNo" runat="server" AutoPostBack="True"
                                                Font-Bold="True" Font-Size="14px"
                                                OnSelectedIndexChanged="ddlPageNo_SelectedIndexChanged"
                                                Style="border-right: navy 1px solid; border-top: navy 1px solid; border-left: navy 1px solid; border-bottom: navy 1px solid"
                                                Width="150px">
                                            </asp:DropDownList>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbldesc" runat="server" Font-Size="12px" Style="font-size: 12px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "subdesc")) %>'
                                                Width="250px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="14px" HorizontalAlign="Left" />
                                        <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Hidden Column" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lbgrcod1" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "subcode")) %>'
                                                Visible="False"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                             <asp:LinkButton ID="lnkbtnEdit" ToolTip="Academic  Name Edit" OnClick="lnkbtnEdit_Click" runat="server" CssClass="btn btn-sm btn-info "><i class="fa fa-edit "></i></asp:LinkButton>

                                            
                                            <%-- <asp:HyperLink ID="hypDelbtn" Target="_blank"  
                                                Visible='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")).Length==0? true:false %>' NavigateUrl="#"                                                
                                                CssClass="btn btn-sm btn-danger " runat="server"><i class="fa fa-trash-alt "></i></asp:HyperLink>--%>


                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
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
                    </div>

                </div>







        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


