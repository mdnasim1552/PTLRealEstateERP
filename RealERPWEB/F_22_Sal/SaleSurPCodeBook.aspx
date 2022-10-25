<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="SaleSurPCodeBook.aspx.cs" Inherits="RealERPWEB.F_22_Sal.SaleSurPCodeBook" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">



    <script type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });


        function loadModal() {
            $('#AddAccCode').modal('toggle', {
                backdrop: 'static',
                keyboard: false
            });
        }




        function CloseModal() {
            $('#AddAccCode').modal('hide');


        }

        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });




            $('#Chboxchild').change(function () {

                var result = $('#Chboxchild').is(':checked');
                var description = result ? "Add Child" : "Add Group";
                $('#lblchild').html(description);


            });

            $('.chzn-select').chosen({ search_contains: true });
        };





    </script>



    <style>
        .switch {
            position: relative;
            display: inline-block;
            width: 40px;
            height: 20px;
        }

            .switch input {
                opacity: 0;
                width: 0;
                height: 0;
            }

        .slider {
            position: absolute;
            cursor: pointer;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            background-color: #ccc;
            -webkit-transition: .4s;
            transition: .4s;
        }

            .slider:before {
                position: absolute;
                content: "";
                height: 18px;
                width: 18px;
                left: 1px;
                bottom: 1px;
                background-color: white;
                -webkit-transition: .4s;
                transition: .4s;
            }

        input:checked + .slider {
            background-color: #2196F3;
        }

        input:focus + .slider {
            box-shadow: 0 0 1px #2196F3;
        }

        input:checked + .slider:before {
            -webkit-transform: translateX(18px);
            -ms-transform: translateX(18px);
            transform: translateX(18px);
        }

        /* Rounded sliders */
        .slider.round {
            border-radius: 20px;
        }

            .slider.round:before {
                border-radius: 50%;
            }
    </style>




    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="card card-fluid">
                <div class="card-body">

                    <div class="row">
                        <div class="col-md-1">
                            <div class="form-group">
                                <label class="control-label  lblmargin-top9px" for="FromDate" id="lblDaterange" runat="server">Search</label>

                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">

                                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control"></asp:TextBox>


                            </div>

                        </div>

                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:LinkButton ID="lnksearch" runat="server" CssClass="btn btn-primary" OnClick="lnksearch_Click"><span class="fa fa-search"></span></asp:LinkButton>
                            </div>
                        </div>



                        <div class="col-md-1">
                            <div class="form-group">
                                <label class="control-label  lblmargin-top9px" for="lblPage" id="lblPage" runat="server">Page</label>



                            </div>
                        </div>



                        <div class="col-md-1">
                            <div class="form-group">

                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control"
                                    OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" >
                                     <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>15</asp:ListItem>
                                    <asp:ListItem>20</asp:ListItem>
                                    <asp:ListItem>30</asp:ListItem>
                                    <asp:ListItem Selected="True">50</asp:ListItem>
                                    <asp:ListItem>100</asp:ListItem>
                                    <asp:ListItem>150</asp:ListItem>
                                    <asp:ListItem>200</asp:ListItem>
                                    <asp:ListItem>300</asp:ListItem>
                                    <asp:ListItem >600</asp:ListItem>
                                    <asp:ListItem>900</asp:ListItem>
                                </asp:DropDownList>

                            </div>
                        </div>








                    </div>


                    <div class="row">
                        <asp:GridView ID="grvacc" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea" AutoGenerateColumns="False" OnRowCancelingEdit="grvacc_RowCancelingEdit" OnRowEditing="grvacc_RowEditing"
                            OnRowUpdating="grvacc_RowUpdating"
                            OnPageIndexChanging="grvacc_PageIndexChanging" BorderStyle="None" OnRowDataBound="grvacc_RowDataBound" OnDataBound="grvacc_DataBound" ShowFooter="false">

                            <FooterStyle BackColor="#5F9467" />









                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No." HeaderStyle-Width="30px">
                                    <ItemTemplate>
                                        <asp:Label ID="serialnoid" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="+">

                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnAdd" runat="server" CssClass="btn btn-xs btn-default" ToolTip="Add New Code" BackColor="Transparent" Visible="false" OnClick="lbtnAdd_Click"><span class="fa fa-plus" aria-hidden="true"></span></asp:LinkButton>

                                        <%--data-toggle="modal" data-target="#detialsinfo"--%>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" Width="20px" HorizontalAlign="Center" />

                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                   <asp:CommandField DeleteText="" HeaderText="Edit" InsertText="" NewText="" HeaderStyle-Width="50px"
                                        SelectText="" ShowEditButton="True" EditText="&lt;i class=&quot;fa fa-edit&quot; aria-hidden=&quot;true&quot;&gt;&lt;/i&gt;">
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                        <ItemStyle ForeColor="#0000C0" />
                                    </asp:CommandField>


                              
                                <asp:TemplateField HeaderText=" " HeaderStyle-Width="30px">
                                    <EditItemTemplate>
                                        <asp:Label ID="lbgrcode" runat="server" CssClass="disabled"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode2"))+"-" %>'></asp:Label>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label7" runat="server" CssClass="disabled"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode2"))+"-" %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="30px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Project Code" HeaderStyle-Width="90px">

                                    <ItemTemplate>

                                        <asp:Label ID="txtgrcode" runat="server" BackColor="Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode4")) %>'></asp:Label>

                                        <asp:Label ID="lbgrcod1" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode3")) %>'
                                            Visible="False"></asp:Label>


                                    </ItemTemplate>
                                    <HeaderStyle />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvDesc" runat="server" CssClass="form-control inputTxt"
                                            Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'></asp:TextBox>
                                    </EditItemTemplate>
                                   
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnkgvactdesc" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                            Font-Size="11px" Style="text-align: left; background-color: Transparent; color: Black;"
                                            Font-Underline="false" Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc"))  %>'
                                            Width="300px">                                             
                                            
                                        </asp:HyperLink>
                                        <%--<asp:Hyperlink ID="Label3" runat="server" Style="font-size: 12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'></asp:Hyperlink>--%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <HeaderStyle />
                                </asp:TemplateField>






                             
                                <asp:TemplateField HeaderText="Short Description">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvTypeDesc" runat="server" CssClass="form-control inputTxt" MaxLength="100"
                                            Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "acttdesc")) %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblacttdesc" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "acttdesc")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="100px" HorizontalAlign="Left" />
                                    <ControlStyle Width="100px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Entry User Name" Visible="false">

                                    <ItemTemplate>
                                        <asp:Label ID="lblgvUsr" runat="server" Style="font-size: 12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "userdesc")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                              

                           
                           



                                <asp:TemplateField HeaderText="Company">
                                    <EditItemTemplate>
                                        <asp:Panel ID="pnlCompany" runat="server" BorderColor="Yellow" BorderStyle="Solid"
                                            BorderWidth="1px">


                                            <table style="width: 100%;">
                                                <tr>

                                                    <td>
                                                        <asp:DropDownList ID="ddlcompany" runat="server" CssClass=" chzn-select ddlPage" Width="180px" TabIndex="6">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvpactdesc" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "companydesc")) %>'
                                            Width="180px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="150px" />
                                </asp:TemplateField>
                            </Columns>




                            <PagerTemplate>
                                <table id="pagerOuterTable" class="pagerOuterTable" runat="server">
                                    <tr>
                                        <td>
                                            <table id="pagerInnerTable" cellpadding="2" cellspacing="1" runat="server">
                                                <tr>
                                                    <td class="pageCounter">
                                                        <asp:Label ID="lblPageCounter" runat="server" Text=""></asp:Label>
                                                    </td>
                                                    <td class="pageFirstLast">
                                                        <img src="../Image/firstpage.gif" align="absmiddle" />&nbsp;<asp:LinkButton ID="lnkFirstPage" CssClass="pagerLink" runat="server" CommandName="Page" CommandArgument="First">First</asp:LinkButton>
                                                    </td>
                                                    <td class="pagePrevNextNumber">
                                                        <asp:ImageButton ID="imgPrevPage" runat="server" ImageAlign="AbsMiddle" ImageUrl="../Image/prevpage.gif" CommandName="Page" CommandArgument="Prev" />
                                                    </td>

                                                    <td class="pagePrevNextNumber">
                                                        <asp:ImageButton ID="imgNextPage" runat="server" ImageAlign="AbsMiddle" ImageUrl="../Image/nextpage.gif" CommandName="Page" CommandArgument="Next" />
                                                    </td>
                                                    <td class="pageFirstLast">
                                                        <asp:LinkButton ID="lnkLastPage" CssClass="pagerLink" CommandName="Page" CommandArgument="Last" runat="server">Last</asp:LinkButton>&nbsp;<img src="../Image/lastpage.gif" align="absmiddle" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td visible="false" class="pageGroups">Pages:&nbsp;<asp:DropDownList ID="ddlPageGroups" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPageGroups_SelectedIndexChanged"></asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </PagerTemplate>


                             <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="" />
                                <HeaderStyle CssClass="" />

                        </asp:GridView>


                    </div>
                </div>
            </div>









            <%--Modal  --%>


            <div id="AddAccCode" class="modal animated slideInLeft " role="dialog" data-keyboard="false" data-backdrop="static">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content  ">
                        <div class="modal-header">
                            <h5 class="modal-title"><i class="fas fa-info-circle"></i>&nbsp;Add New Code</h5>
                            <asp:Label ID="lblmobile" runat="server"></asp:Label>
                            <button type="button" class="btn btn-xs btn-danger float-right" data-dismiss="modal" title="Close"><i class="fas fa-times-circle"></i></button>
                        </div>
                        <div class="modal-body form-horizontal">
                            <div class="row mb-1">
                                  <asp:Label ID="lblactcode" runat="server" Visible="false"></asp:Label>
                                <label class="col-md-4">Project Code </label>
                                <div class="col-md-8">
                                    
                                    <asp:TextBox ID="txtacountcode" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>

                            </div>
                            <div class="row mb-1">

                                <label class="col-md-4">Project Descrioption</label>
                                <div class="col-md-8">
                                      <asp:TextBox ID="txtaccounthead" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>

                            </div>
                            
                          

                            <div class="row mb-1">
                                <label class="col-md-4">Short Description  </label>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtshort" runat="server" CssClass="form-control"></asp:TextBox>
                                   
                                </div>
                                <div class="col-md-1">
                                    <label id="chkbod" runat="server" class="switch">
                                        <asp:CheckBox ID="Chboxchild" runat="server" ClientIDMode="Static" />
                                        <span class="btn btn-xs slider round"></span>
                                    </label>
                                    <%--<asp:Label ID="lblchild" runat="server" Text="Add Child" CssClass="btn btn-xs" ClientIDMode="Static"></asp:Label>--%>
                                </div>
                                <div class="col-md-2">
                                    <asp:Label ID="lblchild" runat="server" Text="Add Child" CssClass="btn btn-xs" ClientIDMode="Static"></asp:Label>
                                </div>

                            </div>
                          
                        
                            <div class="row mb-1">
                                <label id="lblddlcompany" runat="server" class="col-md-4">Company</label>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="ddladdCompany" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                          
                        </div>
                        <div class="modal-footer ">
                            <asp:LinkButton ID="lbtnAddCode" runat="server" CssClass="btn btn-sm btn-success" OnClientClick="CloseModal();" OnClick="lbtnAddCode_Click" ToolTip="Update Code Info.">
                                <i class="fas fa-save"></i>&nbsp;Update </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>


         



        </ContentTemplate>
    </asp:UpdatePanel>




</asp:Content>


