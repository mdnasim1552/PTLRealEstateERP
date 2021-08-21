
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="CustUtility.aspx.cs" Inherits="RealERPWEB.F_24_CC.CustUtility" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });
            $('.chzn-select').chosen({ search_contains: true });
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
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_B">

                            <div class="form-horizontal">

                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName" Text="Add.No"></asp:Label>
                                        <asp:Label ID="lblCurNo1" runat="server" CssClass="smltxtBox"></asp:Label>
                                        <asp:Label ID="lblCurNo2" runat="server" CssClass="smltxtBox60px"></asp:Label>

                                    </div>
                                   
                                    <div class="col-md-2 pading5px asitCol3">
                                        <asp:Label ID="Label5" runat="server" CssClass="smLbl_to" Text="Date"></asp:Label>

                                        <asp:TextBox ID="txtCurTransDate" runat="server" CssClass="inputTxt inputDateBox"></asp:TextBox>
                                         <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtCurTransDate">
                                        </cc1:CalendarExtender>

                                    </div>
                           
                                  
                                    <div class="col-md-3 pading5px pull-right" >


                                      
                                        <asp:LinkButton ID="ibtnPreAdNo"  runat="server" OnClick="ibtnPreAdNo_Click" TabIndex="2">Previous</asp:LinkButton>

                                         
                                        <asp:DropDownList ID="ddlPrevADNumber" runat="server" CssClass="inputTxt" TabIndex="3" Width="120px"></asp:DropDownList>
                                    </div>   
                                                                       

                                    </div>
                                   

                               
                               

                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="lblPreList" runat="server" CssClass="lblTxt lblName" Text="Project Name"></asp:Label>
                                        <asp:TextBox ID="txtSrcPro" runat="server" CssClass="inputTxt inputDateBox"></asp:TextBox>
                                        <asp:LinkButton ID="ibtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindProject_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                    </div>
                                    <div class="col-md-4 pading5px asitCol4">

                                        <asp:DropDownList ID="ddlProjectName" runat="server" AutoPostBack="true" CssClass="chzn-select form-control inputTxt" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged" TabIndex="3"></asp:DropDownList>
                                        <asp:Label ID="lblProjectdesc" runat="server" CssClass="form-control inputTxt" Visible="False"></asp:Label>
                                         
                                    </div>
                                   <div>
                                       <asp:LinkButton ID="lbtnOk" runat="server" OnClick="lbtnOk_Click" CssClass="btn btn-primary okBtn"
                                            TabIndex="7">Ok</asp:LinkButton>
                                   </div>
                                   
                                      <div class="col-md-3 pading5px">
                                        <asp:Label ID="lblSchCode" runat="server" Visible="False"></asp:Label>
                                    </div> 
                                    <div class="clearfix"></div>

                                </div>

                                <div class="form-group">
                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label3" runat="server" CssClass="lblTxt lblName" Text="Unit Name"></asp:Label>


                                        <asp:TextBox ID="txtsrchUnitName" runat="server" CssClass="inputTxt inputDateBox"></asp:TextBox>
                                        <asp:LinkButton ID="ibtnFindUnitName" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindUnitName_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                    </div>
                                    <div class="col-md-4 pading5px asitCol4">

                                        <asp:DropDownList ID="ddlUnitName" runat="server" CssClass="form-control inputTxt" OnSelectedIndexChanged="ddlUnitName_SelectedIndexChanged"></asp:DropDownList>
                                        
                                        <asp:Label ID="lblUnitName" runat="server" CssClass="form-control inputTxt" Visible="False"></asp:Label>

                                    </div>

                                    <div class="clearfix"></div>

                                </div>

                            </div>
                        </fieldset>

                    </div>
                   
                        <div class="row">

                            <asp:Panel ID="PanelItem" runat="server" Visible="False">
                                <fieldset class="scheduler-border fieldset_B">

                                    <div class="form-horizontal">

                                        <div class="form-group">
                                            <div class="col-md-3 pading5px asitCol3">
                                                <asp:Label ID="lblItem" runat="server" CssClass="lblTxt lblName" Text="Resource List"></asp:Label>
                                                  <asp:TextBox ID="txtsrchItem" runat="server" CssClass="inputTxt inputDateBox"></asp:TextBox>
                                        <asp:LinkButton ID="ibtnFindAdWork" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindAdWork_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>


                                            </div>
                                            <div class="col-md-4 pading5px asitCol4">

                                                <asp:DropDownList ID="ddlItemName" runat="server" CssClass="chzn-select form-control inputTxt" TabIndex="3"></asp:DropDownList>

                                            </div>
                                            <div class="col-md-1 pading5px">
                                                <asp:LinkButton ID="lbtnAddWork" CssClass="btn btn-primary primaryBtn" runat="server" OnClick="lbtnAddWork_Click" TabIndex="2">Add </span></asp:LinkButton>

                                            </div>
                                            <div class="col-md-3">
                                                <asp:Label ID="lmsg" runat="server" CssClass="btn btn-danger primaryBtn" ></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>





                            </asp:Panel>
                        </div>


             
                    <div class="row">
                        <div class="table table-responsive">
                        <asp:GridView ID="gvAddWork" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea "
                            ShowFooter="True" Width="543px">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo3" runat="server" Font-Bold="True" ForeColor="Black"
                                            Height="16px" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvGcodAdd" runat="server" ForeColor="Black" Height="16px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                            Width="49px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnTotalAddWork" runat="server" CssClass="btn btn-primary  btn-xs" OnClick="lbtnTotalAddWork_Click">Total</asp:LinkButton>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description of Item">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lFinalUpdateAdWork" runat="server"  CssClass="btn btn-danger btn-xs" OnClick="lFinalUpdateAdWork_Click">Final Update</asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgdescw" runat="server" ForeColor="Black"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                            Width="200px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                              


                             



                                 <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvclAmt" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px" Height="18px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "clamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>

                                   
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFclAmt" runat="server"></asp:Label>
                                    </FooterTemplate>

                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
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


                      <asp:Panel ID="PnlNarration" runat="server" Visible="False">

                            <fieldset class="scheduler-border fieldset_Nar">
                                <div class="form-horizontal">

                                    <div class="form-group">
                                        <div class="col-md-6 pading5px">
                                            <div class="input-group">
                                                <span class="input-group-addon glypingraddon">
                                                    <asp:Label ID="lblNarr" runat="server" CssClass="lblTxt" Text="Narration:"></asp:Label>
                                                </span>
                                                <asp:TextBox ID="txtNarr" runat="server" class="form-control" TextMode="MultiLine" Height="40px"></asp:TextBox>
                                            </div>
                                        </div>

                                      

                                        <div class="clearfix"></div>
                                    </div>

                              

                                </div>

                            </fieldset>
                          </asp:Panel>



                </div>
            </div>






            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

