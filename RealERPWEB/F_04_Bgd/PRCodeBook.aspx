<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="PRCodeBook.aspx.cs" Inherits="RealERPWEB.F_04_Bgd.PRCodeBook" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
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

            <div class="card card-fluid">
                <div class="card-body" style="min-height: 450px;">

                    <div class="row">

                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="control-label">SELECT CODE</label>
                                <asp:DropDownList ID="ddlOthersBook" runat="server" CssClass="custom-select chzn-select">
                                </asp:DropDownList>

                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <label class="control-label">Segment</label>
                                <asp:DropDownList ID="ddlOthersBookSegment" runat="server" CssClass="custom-select chzn-select">
                                    <asp:ListItem Value="2">Sub Code-1</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="5">Details Code</asp:ListItem>
                                </asp:DropDownList>
                               




                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:LinkButton ID="lnkok" runat="server" CssClass="margin-top30px btn btn-primary" OnClick="lnkok_Click">Ok</asp:LinkButton>

                                <asp:Label ID="ConfirmMessage" runat="server" CssClass="btn-danger btn-sm btn" Visible="false"></asp:Label>


                            </div>
                        </div>

                    </div>



                    

                    <asp:GridView ID="grvacc" runat="server" CssClass=" table-condensed table-hover table-bordered grvContentarea"
                        AutoGenerateColumns="False" 
                         CellPadding="4" Font-Bold="False" Font-Size="12px"
                        OnRowCancelingEdit="grvacc_RowCancelingEdit" OnRowEditing="grvacc_RowEditing"
                        OnRowUpdating="grvacc_RowUpdating" Width="572px" ShowFooter="True">
                        <PagerSettings NextPageText="Next" PreviousPageText="Previous"
                            Visible="False" />
                        <FooterStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl">
                                <ItemTemplate>
                                    <asp:Label ID="lblserialnoid" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                <ItemStyle Font-Size="12px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="+">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnAdd" runat="server" CssClass="btn btn-xs btn-default" ToolTip="Add New Code" BackColor="Transparent"
                                            Visible='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "additem"))=="True"?true:false %>' OnClick="lbtnAdd_Click"><span class="fa fa-plus" aria-hidden="true"></span></asp:LinkButton>
                                        <%--data-toggle="modal" data-target="#detialsinfo"--%>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" Width="20px" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:CommandField DeleteText="" HeaderText="Edit" InsertText="" NewText=""
                                SelectText="" ShowEditButton="True" EditText="&lt;i class=&quot;fa fa-edit&quot; aria-hidden=&quot;true&quot;&gt;&lt;/i&gt;">
                                <HeaderStyle Font-Size="16px" />
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
                                    <asp:TextBox ID="txtgrcode" runat="server" Font-Size="12px" Height="16px"
                                        MaxLength="3"
                                        Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prgcod3")) %>'
                                        Width="40px"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbgrcod3" runat="server" Font-Size="12px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prgcod3")) %>'
                                        Width="40px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle  Font-Size="16px" />
                                <ItemStyle Font-Size="12px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description of Code">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtgvDesc" runat="server" Font-Size="12px" MaxLength="100"
                                        Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prgdesc")) %>'
                                        Width="250px"></asp:TextBox>
                                </EditItemTemplate>

                                <ItemTemplate>
                                    <asp:Label ID="lbldesc" runat="server" Font-Size="12px"
                                        Style="font-size: 12px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prgdesc")) %>'
                                        Width="250px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Size="16px" HorizontalAlign="Left" />
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
                                    <asp:TextBox ID="txtgvttpe" runat="server" BackColor="White" BorderStyle="None"
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
                            <asp:TemplateField HeaderText="Serial">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtgvtslno" runat="server" BackColor="White" BorderStyle="None"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "slno")) %>'
                                        Width="30px"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lgvslno" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "slno")) %>'
                                        Width="30px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Code">
                                <EditItemTemplate>
                                    <asp:Panel ID="Panel21" runat="server">
                                        <table style="width: 100%;">
                                            <tr>

                                                <td>
                                                    <div class="col-md-4 pading5px">
                                                        <asp:DropDownList ID="ddlData" runat="server" CssClass="form-control inputTxt chzn-select" Width="200px">
                                                        </asp:DropDownList>
                                                    </div>

                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblgvcodedesc" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "codedesc")) %>'
                                        Width="150px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>


                             <asp:TemplateField HeaderText="Visibility">
                                            
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkvisibility" runat="server"
                                                    Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "visibility"))=="True" %>'
                                                    Width="50px" />
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                        </asp:TemplateField>



                              <asp:TemplateField HeaderText="Unit">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtgvunit" runat="server" BackColor="White" BorderStyle="None"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                        Width="80px"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lgvunit" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                        Width="30px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                        </Columns>
                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                        <FooterStyle CssClass="grvFooter" />
                                        <RowStyle CssClass="grvRows" />
                    </asp:GridView>

                </div>
            </div>



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
                                <asp:Label ID="lblprgcode" runat="server" Visible="false"></asp:Label>
                                <label class="col-md-4">PRG Code </label>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtprgcode" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                         
                            <div class="row mb-1">
                                <label class="col-md-4">Description of Code</label>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtprgdesc" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>  
                            <div>
                                <asp:TextBox Visible="false" ID="prgcode" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="row mb-1">
                                <label class="col-md-4">Type </label>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtprgtype" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <%--<div class="row mb-1">
                                <label class="col-md-4">Unit</label>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtprgunit" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div> --%>
                            <div class="row mb-1">
                                <label class="col-md-4">Visibility </label>
                                <div class="col-md-8">
                                    <asp:CheckBox ID="chkprgvisibility" runat="server"/>
                                    <%--<asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"></asp:TextBox>--%>
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


