<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="HRDesigCode.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_82_App.HRDesigCode" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        div#ContentPlaceHolder1_ddlOthersBook_chzn {
            width: 100% !important;
        }

        .mt20 {
            margin-top: 20px;
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
        function loadModalAddCode() {
            $('#AddResCode').modal('toggle', {
                backdrop: 'static',
                keyboard: false
            });
        };
        function CloseModalAddCode() {
            $('#AddResCode').modal('hide');
        };

    </script>





    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="card mt-5">
                <div class="card-header">
                    <div class="row">
                        <div class="col-lg-3 col-md-3 col-sm-6">
                            <div class="form-group">
                                <asp:Label ID="LblBookName" runat="server" Text="Select Code Book:"></asp:Label>
                                <asp:DropDownList ID="ddlOthersBook" runat="server" CssClass="form-control chzn-select">
                                </asp:DropDownList>
                                <asp:Label ID="lbalterofddl" runat="server" Visible="False" CssClass="form-control "></asp:Label>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-6">
                            <asp:Label ID="Label1" runat="server" Text="Details Code"></asp:Label>
                            <asp:DropDownList ID="ddlOthersBookSegment" CssClass="form-control form-control-sm" runat="server">
                                <asp:ListItem Selected="True" Value="7">Details Code</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Label ID="lbalterofddlSegment" runat="server" Visible="False" CssClass="form-control inputTxt"></asp:Label>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-6">
                            <asp:Label ID="Label2" runat="server" Text="">
                                <asp:LinkButton ID="ibtnSrch" runat="server" OnClick="ibtnSrch_Click"><i class="fa fa-search"> </i></asp:LinkButton>

                            </asp:Label>
                            <asp:TextBox ID="txtDesignationSrc" runat="server" Text="" CssClass="form-control form-control-sm"></asp:TextBox>

                        </div>
                        <div class="col-lg-1">
                                              <div class="form-group">
                                <asp:Label ID="Label3" runat="server">Page Size</asp:Label>


                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>15</asp:ListItem>
                                    <asp:ListItem>20</asp:ListItem>
                                    <asp:ListItem>30</asp:ListItem>
                                    <asp:ListItem>50</asp:ListItem>
                                    <asp:ListItem>100</asp:ListItem>
                                    <asp:ListItem>150</asp:ListItem>
                                    <asp:ListItem>200</asp:ListItem>
                                    <asp:ListItem Selected="True">300</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-6">
                            <asp:LinkButton ID="lnkok" runat="server" Text="Ok" OnClick="lnkok_Click" CssClass="btn btn-primary btn-sm mt20"></asp:LinkButton>

                        </div>



                        <div class="col-lg-1 col-md-1 col-sm-6">
                            <asp:Label ID="ConfirmMessage" CssClass="btn-danger btn btn-sm" runat="server" Visible="false"></asp:Label>

                        </div>

                    </div>
                </div>
                <div class="card-body">
                    <asp:GridView ID="grvacc" runat="server" AllowPaging="True"
                        AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea" 
                        OnRowCancelingEdit="grvacc_RowCancelingEdit" OnRowEditing="grvacc_RowEditing" 
                        OnRowUpdating="grvacc_RowUpdating" ShowFooter="True" Width="600px" OnPageIndexChanging="grvacc_PageIndexChanging"
                        PageSize="15">
                        <PagerSettings NextPageText="Next" PreviousPageText="Previous"
                            Visible="False" />
                        <%-- <FooterStyle BackColor="#5F9467" Font-Bold="True" ForeColor="White" />--%>
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.">
                                <ItemTemplate>
                                    <asp:Label ID="lblserialnoid" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"
                                      > </asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                <ItemStyle Font-Size="12px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="+">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnAdd" runat="server" CssClass="btn btn-xs btn-default" ToolTip="Add New Code" BackColor="Transparent"
                                        Visible='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "additem"))=="1"?true:false %>' OnClick="lbtnAdd_Click"><span class="fa fa-plus" aria-hidden="true"></span></asp:LinkButton>
                                    <%--data-toggle="modal" data-target="#detialsinfo"--%>
                                </ItemTemplate>
                            <HeaderStyle Font-Bold="True" Font-Size="16px" Width="20px" HorizontalAlign="Center" />
                             <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:CommandField DeleteText="" HeaderText="Edit" InsertText="" NewText=""
                                SelectText="" ShowEditButton="True" EditText="&lt;i class=&quot;fa fa-edit&quot; aria-hidden=&quot;true&quot;&gt;&lt;/i&gt;">
                                <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                <ItemStyle ForeColor="#0000C0" />
                            </asp:CommandField>
                            <asp:TemplateField HeaderText=" ">
                                <ItemTemplate>
                                    <asp:Label ID="lblgrcode" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "hrgcod2"))+"-" %>'
                                        Width="20px"   CssClass='<%#(Convert.ToString(DataBinder.Eval(Container.DataItem, "hrgcod")).Remove(1,4)=="000") ? "bg-danger text-white": "" %>' ></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Font-Size="12px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Code">
                                <ItemTemplate>
                                    <asp:Label ID="lbgrcod3" runat="server" Font-Size="12px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "hrgcod3")) %>' Width="40px"
                                         CssClass='<%#(Convert.ToString(DataBinder.Eval(Container.DataItem, "hrgcod")).Remove(1,4)=="000") ? "bg-danger text-white": "" %>' >

                                    </asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                <ItemStyle Font-Size="12px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description of Code">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtgvDesc" runat="server" Font-Size="12px" MaxLength="100"
                                        Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "hrgdesc")) %>'
                                        Width="250px"></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:DropDownList ID="ddlPageNo" runat="server" AutoPostBack="True"
                                        Font-Bold="True" Font-Size="14px"
                                        OnSelectedIndexChanged="ddlPageNo_SelectedIndexChanged"
                                        Style="border-right: navy 1px solid; border-top: navy 1px solid; border-left: navy 1px solid; border-bottom: navy 1px solid"
                                        Width="150px" Visible="false">
                                    </asp:DropDownList>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbldesc" runat="server" Font-Size="12px" Style="font-size: 12px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "hrgdesc")) %>'
                                          CssClass='<%#(Convert.ToString(DataBinder.Eval(Container.DataItem, "hrgcod")).Remove(1,4)=="000") ? "bg-danger text-white": "" %>'  
                                        Width="250px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Left" />
                                <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description of Code BN">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtgvDescbn" runat="server" Font-Size="12px" MaxLength="100"
                                        Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "hrgdescbn")) %>'
                                        Width="250px"></asp:TextBox>
                                </EditItemTemplate>

                                <ItemTemplate>
                                    <asp:Label ID="lbldescbn" runat="server" Font-Size="12px" Style="font-size: 12px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "hrgdescbn")) %>'
                                        Width="250px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Left" />
                                <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Hidden Column" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lbgrcod1" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "hrgcod")) %>'
                                        Visible="False"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Type">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtgvttpe" runat="server" BackColor="White" BorderStyle="None"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "hrgval")) %>'
                                        Width="50px"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lgvtype" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "hrgval")) %>'
                                        Width="50px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center"  Font-Size="16px" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Rank">
                                <EditItemTemplate>
                                    <asp:Panel ID="Panel21" runat="server">
                                        <table style="width: 100%;">
                                            <tr>

                                                <td>
                                                    <div class="col-md-4 pading5px">
                                                        <asp:DropDownList ID="ddlRank" runat="server" CssClass="form-control inputTxt chzn-select" Width="200px">
                                                        </asp:DropDownList>
                                                    </div>

                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblgvrank" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rankdesc")) %>'
                                        Width="150px"></asp:Label>
                                </ItemTemplate>
                                 <HeaderStyle HorizontalAlign="Center"  Font-Size="16px" />
                            </asp:TemplateField>

                        </Columns>
         
                        <HeaderStyle CssClass="grvHeader" />
                        <FooterStyle CssClass="grvFooter" />
                        <AlternatingRowStyle BackColor="" />
                    </asp:GridView>

                </div>

                <%-- <fieldset class="scheduler-border fieldset_A">

                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-2 pading5px">
                                        <asp:Label ID="LblBookName" runat="server" CssClass="lblTxt lblName160" Text="Select Code Book:"></asp:Label>
                                    </div>
                                    <div class="col-md-4 pading5px">
                                        <asp:DropDownList ID="ddlOthersBook" runat="server" CssClass="form-control inputTxt">
                                        </asp:DropDownList>
                                        <asp:Label ID="lbalterofddl" runat="server" Visible="False" CssClass="form-control inputTxt"></asp:Label>
                                    </div>
                                    <div class="col-md-2 pading5px">
                                        <asp:DropDownList ID="ddlOthersBookSegment" CssClass="form-control inputTxt" runat="server">
                                            <asp:ListItem Selected="True" Value="7">Details Code</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Label ID="lbalterofddlSegment" runat="server" Visible="False" CssClass="form-control inputTxt"></asp:Label>
                                    </div>
                                    <div class="col-md-1 pading5px">
                                        <asp:LinkButton ID="lnkok" runat="server" Text="Ok" OnClick="lnkok_Click" CssClass="btn btn-primary primaryBtn"></asp:LinkButton>



                                    </div>
                                    <div class="col-md-2 pading5px">
                                        <div class="msgHandSt">
                                            <asp:Label ID="lblmsg" CssClass="btn-danger btn primaryBtn" runat="server" Visible="false"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-2 pading5px">
                                    </div>

                                    <div class="col-md-4 pading5px">
                                        <asp:TextBox ID="txtDesignationSrc" runat="server" CssClass="inputtextbox" Text=""></asp:TextBox>
                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="ibtnSrch" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ibtnSrch_Click" TabIndex="11"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                        </div>
                                    </div>



                                </div>

                            </div>
                        </fieldset>--%>
                <div id="AddResCode" class="modal animated slideInLeft " role="dialog" data-keyboard="false" data-backdrop="static">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content  ">
                        <div class="modal-header">
                            <h5 class="modal-title"><i class="fas fa-info-circle"></i>&nbsp;Add New Code</h5>
                            <asp:Label ID="lblmobile" runat="server"></asp:Label>
                            <button type="button" class="btn btn-xs btn-danger float-right" data-dismiss="modal" title="Close"><i class="fas fa-times-circle"></i></button>
                        </div>
                        <div class="modal-body form-horizontal">
                            <div class="row mb-1">
                                <asp:Label ID="lbgrcod" runat="server" Visible="false"></asp:Label>
                                <label class="col-md-4"> Code  </label>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txthrgcode" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div>
                                <asp:TextBox Visible="false" ID="hrgcodechk" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="row mb-1">
                                <label class="col-md-4">Description of Code</label>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtDesc" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>  
                            <div class="row mb-1">
                                <label class="col-md-4">Description of Code BN</label>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtDescBN" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div> 
                            <div class="row mb-1">
                                <label class="col-md-4">Type </label>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txttype" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="modal-footer ">
                            <asp:LinkButton ID="lbtnAddCode" runat="server" CssClass="btn btn-sm btn-success" OnClientClick="CloseModalAddCode();" OnClick="lbtnAddCode_Click"  ToolTip="Update Code Info.">
                                <i class="fas fa-save"></i>&nbsp;Update </asp:LinkButton>
                            </div>

                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


