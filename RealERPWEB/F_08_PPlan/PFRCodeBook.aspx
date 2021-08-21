<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="PFRCodeBook.aspx.cs" Inherits="RealERPWEB.F_08_PPlan.PFRCodeBook" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />
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
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <asp:Panel ID="Panel1" runat="server">
                                    <div class="form-group">
                                        <div class="col-md-11 pading5px asitCol11">
                                            <asp:Label ID="LblBookName1" runat="server" CssClass=" smLbl_to" Text="SELECT CODE BOOK :"></asp:Label>

                                            <asp:DropDownList ID="ddlOthersBook" runat="server" Width="300" CssClass="ddlPage" TabIndex="2"></asp:DropDownList>

                                            <asp:DropDownList ID="ddlOthersBookSegment" runat="server" CssClass="ddlPage">
                                                <asp:ListItem Value="2">Main</asp:ListItem>
                                                <asp:ListItem Value="4">Sub Code-1</asp:ListItem>
                                                <asp:ListItem Selected="True" Value="7">Details Code</asp:ListItem>
                                            </asp:DropDownList>

                                            <asp:LinkButton ID="lnkok" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lnkok_Click">Ok</asp:LinkButton>


                                            <asp:Label ID="lblPage" runat="server" CssClass="col-md-2 control-label lblTxt" Text="Page Size" Visible="False"></asp:Label>
                                            <div class="col-md-1 pading5px">
                                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control inputTxt"
                                                    OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Visible="False">
                                                    <asp:ListItem>15</asp:ListItem>
                                                    <asp:ListItem>20</asp:ListItem>
                                                    <asp:ListItem>30</asp:ListItem>
                                                    <asp:ListItem>50</asp:ListItem>
                                                    <asp:ListItem>100</asp:ListItem>
                                                    <asp:ListItem>150</asp:ListItem>
                                                    <asp:ListItem>200</asp:ListItem>
                                                    <asp:ListItem>300</asp:ListItem>
                                                    <asp:ListItem>600</asp:ListItem>
                                                    <asp:ListItem>900</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                            <asp:Label ID="ConfirmMessage" runat="server" CssClass=" btn btn-danger primaryBtn"></asp:Label>

                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </fieldset>
                    </div>
                    <div class="table table-responsive">
                        <asp:GridView ID="grvacc" runat="server" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            AutoGenerateColumns="False" OnPageIndexChanging="grvacc_PageIndexChanging"
                            OnRowCancelingEdit="grvacc_RowCancelingEdit" OnRowEditing="grvacc_RowEditing"
                            OnRowUpdating="grvacc_RowUpdating" Width="526px" ShowFooter="True" OnRowDataBound="grvacc_RowDataBound">
                            <PagerSettings NextPageText="Next" PreviousPageText="Previous"
                              />
                            <FooterStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblserialnoid" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="+">

                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnAdd" runat="server" CssClass="btn btn-xs btn-default" ToolTip="Add New Code" BackColor="Transparent" Visible="false" OnClick="lbtnAdd_Click"><span class="fa fa-plus" aria-hidden="true"></span></asp:LinkButton>


                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" Width="20px" HorizontalAlign="Center" />

                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:CommandField DeleteText="" HeaderText="Edit" InsertText="" NewText=""
                                    SelectText="" ShowEditButton="True" EditText="&lt;i class=&quot;fa fa-pencil-square-o&quot; aria-hidden=&quot;true&quot;&gt;&lt;/i&gt;">
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    <ItemStyle Font-Bold="True" Font-Size="12px" ForeColor="#0000C0" />
                                </asp:CommandField>
                                <asp:TemplateField HeaderText=" ">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgrcode" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prgcod2"))+"-" %>'
                                            Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Code">
                                    <EditItemTemplate>
                                        <asp:Label ID="txtgrcode" runat="server" Font-Size="12px" Height="16px"
                                            MaxLength="7"
                                            Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prgcod3")) %>'
                                            Width="40px"></asp:Label>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbgrcod3" runat="server" Font-Size="12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prgcod3")) %>'
                                            Width="40px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description of Code">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvDesc" runat="server" Font-Size="12px" MaxLength="100"
                                            Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prgdesc")) %>'
                                            Width="350px"></asp:TextBox>
                                    </EditItemTemplate>
                                    
                                    <ItemTemplate>
                                        <asp:Label ID="lbldesc" runat="server" Font-Size="12px"
                                            Style="font-size: 12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prgdesc")) %>'
                                            Width="350px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Left" />
                                    <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Details Description">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvDDesc" runat="server" Font-Size="12px" MaxLength="100"
                                            Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prgddesc")) %>'
                                            Width="350px"></asp:TextBox>
                                    </EditItemTemplate>
                                    
                                    <ItemTemplate>
                                        <asp:Label ID="lblDdesc" runat="server" Font-Size="12px"
                                            Style="font-size: 12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prgddesc")) %>'
                                            Width="350px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Left" />
                                    <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Hidden Column" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgrcod1" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prgcod")) %>'
                                            Visible="False"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Type">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvttpe" runat="server" BorderStyle="None"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prgval")) %>'
                                            Width="30px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvtype" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prgval")) %>'
                                            Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Duration">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtduration" runat="server" BorderStyle="None"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "duration")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px" Style="text-align: right;"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvduration" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "duration")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                            <AlternatingRowStyle />
                        </asp:GridView>
                    </div>
                </div>
            </div>

            <%--Modal  --%>

            <div id="AddAccCode" class="modal animated slideInLeft " role="dialog" data-keyboard="false" data-backdrop="static">
                <div class="modal-dialog">
                    <div class="modal-content  ">
                        <div class="modal-header">

                            <button type="button" style="background-color: red;" class="close btn btn-xs" data-dismiss="modal"><span class="fa fa-close"></span></button>
                            <h4 class="modal-title">
                                <span class="fa fa-table"></span>Add New Code  </h4>
                        </div>
                        <div class="modal-body form-horizontal">
                            <div class="row-fluid">
                                <asp:Label ID="lblactcode" runat="server" Visible="false"></asp:Label>

                                <div class="form-group" runat="server">
                                    <label class="col-md-4">Description Of Code</label>


                                    <div class="col-md-8">
                                        <asp:TextBox ID="txtaccounthead" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-4">Type </label>
                                    <div class="col-md-5">
                                        <asp:TextBox ID="txttype" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>




                                    <div class="col-md-3">
                                        <label id="chkbod" runat="server" class="switch">
                                            <asp:CheckBox ID="Chboxchild" runat="server" />
                                            <span class="btn btn-xs slider round"></span>
                                        </label>
                                        <asp:Label ID="lblchild" runat="server" Text="Add Child" CssClass="btn btn-xs"></asp:Label>
                                    </div>
                                </div>

                                

                                <div class="form-group">
                                    <label class="col-md-4">Details Description </label>
                                    <div class="col-md-8">
                                        <asp:TextBox ID="txtDDesc" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-md-4">Duration </label>
                                    <div class="col-md-8">
                                        <asp:TextBox ID="txtDur" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>



                            </div>


                        </div>
                        <div class="modal-footer ">
                            <asp:LinkButton ID="lbtnAddCode" runat="server" CssClass="btn btn-sm btn-success" OnClientClick="CloseModal();" OnClick="lbtnAddCode_Click"><span class="glyphicon glyphicon-save"></span> Update </asp:LinkButton>



                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


